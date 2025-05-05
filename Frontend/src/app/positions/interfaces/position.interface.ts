import { PositionStatusEnum } from "../enums/position-status.enum";
import { Department } from "../../departments/interfaces/department.interface";
import { Recruiter } from "../../recruiters/interfaces/recruiter.interface";

export interface Position {
    id: string;
    title: string;
    description: string;
    location: string;
    status: PositionStatusEnum;
    recruiterId: string;
    departmentId: string;
    budget: number;
    closingDate?: Date | null;

    recruiter?: Recruiter;
    department?: Department;
}