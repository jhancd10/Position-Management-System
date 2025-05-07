/**
 * Enum representing the possible states of a process or operation.
 * 
 * @enum {number}
 * @property {number} Loading - Indicates that the process is currently in progress.
 * @property {number} Success - Indicates that the process has completed successfully.
 * @property {number} Error - Indicates that the process has encountered an error.
 */
export enum StateStatusEnum {
    Loading,
    Success,
    Error
}