import { StateStatusEnum } from "../enums/state-status.enum";
import { Department } from "../../departments/interfaces/department.interface";
import { Position } from "../../positions/interfaces/position.interface";
import { Recruiter } from "../../recruiters/interfaces/recruiter.interface";

export interface State {
    data: Department[] | Recruiter[] | Position[];
    status: StateStatusEnum;
}