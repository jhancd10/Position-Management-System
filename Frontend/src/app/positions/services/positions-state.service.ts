import { Injectable, inject } from '@angular/core';
import { signalSlice } from 'ngxtension/signal-slice';
import { map } from 'rxjs';
import { StateStatusEnum } from '../../shared/enums/state-status.enum';
import { Position } from '../interfaces/position.interface';
import { State } from '../../shared/interfaces/state.interface';
import { PositionsService } from './positions.service';

@Injectable({
  providedIn: 'root',
})
export class PositionsStateService {
    
  private positionsService = inject(PositionsService);

  private initialState: State = {
    data: [],
    status: StateStatusEnum.Loading,
    error: null,
  };

  public state = signalSlice({
    initialState: this.initialState,
    sources: [
      this.positionsService.getPositions().pipe(
        map((position: Position[]) => ({
          data: position,
          status: StateStatusEnum.Success,
        })),
      ),
    ],
  });
}
