import { inject, Injectable, effect } from '@angular/core';
import { State } from '../../shared/interfaces/state.interface';
import { StateStatusEnum } from '../../shared/enums/state-status.enum';
import { signalSlice } from 'ngxtension/signal-slice';
import { DepartmentsService } from './departments.service';
import { Department } from '../interfaces/department.interface';
import { catchError, map, Observable, of, switchMap } from 'rxjs';

/**
 * @description
 * Service to manage the state of departments in the application. It provides
 * functionality to load, create, update, and delete departments while maintaining
 * a reactive state using `signalSlice`.
 *
 * @class DepartmentsStateService
 * @decorator `@Injectable`
 */
@Injectable({
  providedIn: 'root',
})
export class DepartmentsStateService {

  private departmentsService = inject(DepartmentsService);

  private initialState: State = {
    data: [],
    status: StateStatusEnum.Loading,
    error: null
  };

  private loadDepartments$() {
    
    return this.departmentsService
    .getDepartments()
    .pipe(
      map((departments: Department[]): State => ({
        data: departments,
        status: StateStatusEnum.Success,
        error: null
      })),
      catchError((err) => {

        const errState: State = {
          data: [],
          status: StateStatusEnum.Error,
          error: err
        }

        return of(errState);
      })
    );
  }

  public state = signalSlice({
    
    initialState: this.initialState,
    sources: [ this.loadDepartments$() ],
    actionSources: {
      
      load: () => this.loadDepartments$(),

      create: (state, action$: Observable<Department>) =>
        action$.pipe(
          switchMap((departmentToCreate: Department) =>
            this.departmentsService.createDepartment(departmentToCreate)
            .pipe(
              map((departmentCreated: Department) => ({
                data: [ departmentCreated, ...state().data as Department[] ]
              }))
            )
          ),
        ),
      
      update: (state, action$: Observable<Department>) =>
        action$.pipe(
          switchMap((departmentToUpdate: Department) =>
            this.departmentsService.updateDepartment(departmentToUpdate.id!, departmentToUpdate)
            .pipe(
              map((departmentUpdated: Department) => {

                const filteredDepartments = (state().data as Department[]).filter(
                  (department: Department) => department.id !== departmentUpdated.id
                );

                return {
                  data: [ departmentUpdated, ...filteredDepartments ]
                }
              })
            )
          ),
        ),
      
      delete: (state, action$: Observable<Department>) =>
        action$.pipe(
          switchMap((departmentToDelete: Department) =>
            this.departmentsService.deleteDepartment(departmentToDelete.id!)
            .pipe(
              map(() => {

                const filteredDepartments = (state().data as Department[]).filter(
                  (department: Department) => department.id !== departmentToDelete.id
                );

                return {
                  data: [ ...filteredDepartments ]
                }
              })
            )
          ),
        ),
    }
  });

  constructor() {
    effect(
      () => {
        const created = this.state.createUpdated();
        const updated = this.state.updateUpdated();
        const deleted = this.state.deleteUpdated();

        if (created || updated || deleted) this.state.load();
      }
    );
  }
}
