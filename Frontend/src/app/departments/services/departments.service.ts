import { Injectable } from '@angular/core';
import { BaseHttpService } from '../../shared/services/base-http.service';
import { Observable } from 'rxjs';
import { Department } from '../interfaces/department.interface';

@Injectable({
  providedIn: 'root',
})
export class DepartmentsService extends BaseHttpService {
    
  private uri: string = `${this.apiUrl}/departments`;

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.uri);
  }
}
