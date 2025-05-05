import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class BaseHttpService {

    protected http: HttpClient = inject(HttpClient);   
    protected apiUrl: string = environment.API_URL;
}