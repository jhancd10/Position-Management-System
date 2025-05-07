import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../shared/services/base-http.service';
import { Observable } from 'rxjs';
import { Department } from '../interfaces/department.interface';

@Injectable({
  providedIn: 'root',
})
/**
 * Service for managing department-related operations.
 * Extends the `BaseHttpService` to provide HTTP methods for interacting with the backend API.
 */
export class DepartmentsService extends BaseHttpService {

  /**
   * Base URI for department-related API endpoints.
   * @private
   */
  private uri: string = '/departments';

  /**
   * Retrieves a list of all departments.
   * 
   * @returns An `Observable` that emits an array of `Department` objects.
   */
  getDepartments(): Observable<Department[]> {
    return this.get<Department[]>(this.uri);
  }

  /**
   * Creates a new department.
   * 
   * @param department - The `Department` object containing the details of the department to be created.
   * @returns An `Observable` that emits the created `Department` object.
   */
  createDepartment(department: Department): Observable<Department> {
    return this.post<Department>(this.uri, department);
  }

  /**
   * Updates an existing department.
   * 
   * @param id - The unique identifier of the department to be updated.
   * @param department - The `Department` object containing the updated details.
   * @returns An `Observable` that emits the updated `Department` object.
   */
  updateDepartment(id: string, department: Department): Observable<Department> {
    return this.put<Department>(`${this.uri}/${id}`, department);
  }

  /**
   * Deletes a department by its unique identifier.
   * 
   * @param id - The unique identifier of the department to be deleted.
   * @returns An `Observable` that emits `void` upon successful deletion.
   */
  deleteDepartment(id: string): Observable<void> {
    return this.delete<void>(`${this.uri}/${id}`);
  }
}
