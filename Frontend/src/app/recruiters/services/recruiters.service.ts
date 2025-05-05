import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseHttpService } from "../../shared/services/base-http.service";
import { Recruiter } from "../interfaces/recruiter.interface";

@Injectable({
    providedIn: 'root',
  })
  export class RecruitersService extends BaseHttpService {
      
    private uri: string = `${this.apiUrl}/recruiters`;
  
    getRecruiters(): Observable<Recruiter[]> {
      return this.http.get<Recruiter[]>(this.uri);
    }
  }
  