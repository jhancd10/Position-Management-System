import { StateStatusEnum } from "../enums/state-status.enum";
import { Department } from "../../departments/interfaces/department.interface";
import { Position } from "../../positions/interfaces/position.interface";
import { Recruiter } from "../../recruiters/interfaces/recruiter.interface";

/**
 * Represents the state of a specific entity in the application.
 * This interface is used to manage and track the state of data,
 * including its current status and any associated errors.
 */
export interface State {
    /**
     * The data associated with the state. This can be an array of
     * `Department`, `Recruiter`, or `Position` objects.
     */
    data: Department[] | Recruiter[] | Position[];

    /**
     * The current status of the state, represented as an enumeration.
     * This indicates whether the state is loading, loaded, or in an error state.
     */
    status: StateStatusEnum;

    /**
     * Any error information associated with the state. This can be used
     * to store error messages or objects when an operation fails.
     */
    error: any;
}