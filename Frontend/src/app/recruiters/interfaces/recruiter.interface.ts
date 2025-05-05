import { Position } from "../../positions/interfaces/position.interface";

export interface Recruiter {
    id: string;
    name: string;
    cellphone: string;
    email: string;

    positions?: Position[];
}