import { Position } from "../../positions/interfaces/position.interface";

export interface Department {
    id?: string;
    name: string;
    positions?: Position[];
}