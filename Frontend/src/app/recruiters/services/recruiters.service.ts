import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../shared/services/base-http.service';
import { Recruiter } from '../interfaces/recruiter.interface';

@Injectable({
  providedIn: 'root',
})
export class RecruitersService extends BaseHttpService {
  
  private uri: string = '/recruiters';

  getRecruiters(): Observable<Recruiter[]> {
    return this.get<Recruiter[]>(this.uri);
  }

  createRecruiter(recruiter: Recruiter): Observable<Recruiter> {
    return this.post<Recruiter>(this.uri, recruiter);
  }

  updateRecruiter(id: string, recruiter: Recruiter): Observable<Recruiter> {
    return this.put<Recruiter>(`${this.uri}/${id}`, recruiter);
  }

  deleteRecruiter(id: string): Observable<void> {
    return this.delete<void>(`${this.uri}/${id}`);
  }
}
