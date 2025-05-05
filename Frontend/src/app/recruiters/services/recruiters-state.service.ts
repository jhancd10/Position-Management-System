import { Injectable, inject } from '@angular/core';
import { signalSlice } from 'ngxtension/signal-slice';
import { map } from 'rxjs';
import { StateStatusEnum } from '../../shared/enums/state-status.enum';
import { Recruiter } from '../interfaces/recruiter.interface';
import { State } from '../../shared/interfaces/state.interface';
import { RecruitersService } from './recruiters.service';

@Injectable({
  providedIn: 'root',
})
export class RecruitersStateService {
    
  private recruitersService = inject(RecruitersService);

  private initialState: State = {
    data: [],
    status: StateStatusEnum.Loading,
  };

  public state = signalSlice({
    initialState: this.initialState,
    sources: [
      this.recruitersService.getRecruiters().pipe(
        map((recruiters: Recruiter[]) => ({
          data: recruiters,
          status: StateStatusEnum.Success,
        })),
      ),
    ],
  });
}
