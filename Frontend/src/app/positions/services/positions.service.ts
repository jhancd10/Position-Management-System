import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseHttpService } from '../../shared/services/base-http.service';
import { Position } from '../interfaces/position.interface';

@Injectable({
  providedIn: 'root',
})
export class PositionsService extends BaseHttpService {

  private uri: string = '/positions';

  getPositions(): Observable<Position[]> {
    return this.get<Position[]>(this.uri);
  }

  getPositionById(id: string): Observable<Position[]> {
    return this.get<Position[]>(`${this.uri}/${id}`);
  }

  createPosition(position: Position): Observable<Position> {
    return this.post<Position>(this.uri, position);
  }

  updatePosition(id: string, position: Position): Observable<Position> {
    return this.put<Position>(`${this.uri}/${id}`, position);
  }

  deletePosition(id: string): Observable<void> {
    return this.delete<void>(`${this.uri}/${id}`);
  }
}
