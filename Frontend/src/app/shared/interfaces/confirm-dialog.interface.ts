/**
 * Interface representing the data structure for a confirmation dialog.
 * This interface is used to define the properties required to display
 * a confirmation dialog with customizable text for the title, message,
 * and optional confirm and cancel buttons.
 */
export interface ConfirmDialogData {
  title: string;
  message: string;
  confirmText?: string;
  cancelText?: string;
}
