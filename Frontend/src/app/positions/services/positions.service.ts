import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BaseHttpService } from "../../shared/services/base-http.service";
import { Position } from "../interfaces/position.interface";

@Injectable({
    providedIn: 'root',
  })
  export class PositionsService extends BaseHttpService {
      
    private uri: string = `${this.apiUrl}/positions`;
  
    getPositions(): Observable<Position[]> {
      return this.http.get<Position[]>(this.uri);
    }
  }
  