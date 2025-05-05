import { inject, Injectable } from '@angular/core';
import { State } from '../../shared/interfaces/state.interface';
import { StateStatusEnum } from '../../shared/enums/state-status.enum';
import { signalSlice } from 'ngxtension/signal-slice';
import { DepartmentsService } from './departments.service';
import { Department } from '../interfaces/department.interface';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DepartmentsStateService {

  private departmentsService = inject(DepartmentsService);

  private initialState: State = {
    data: [],
    status: StateStatusEnum.Loading,
  };

  public state = signalSlice({
    initialState: this.initialState,
    sources: [
      this.departmentsService
        .getDepartments()
        .pipe(
          map((departments: Department[]) => ({
            data: departments,
            status: StateStatusEnum.Success,
          }))
        ),
    ],
  });
}
