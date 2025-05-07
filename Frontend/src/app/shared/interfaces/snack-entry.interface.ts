import { MatSnackBarConfig } from '@angular/material/snack-bar';

/**
 * Represents an entry for a snack bar notification.
 * 
 * This interface is used to define the structure of a snack bar entry,
 * which includes a message to be displayed and an optional configuration
 * for customizing the behavior and appearance of the snack bar.
 * 
 * @property message - The message to be displayed in the snack bar.
 * @property config - (Optional) Configuration options for the snack bar,
 * which can include settings such as duration, panel class, and more.
 * This is based on the `MatSnackBarConfig` provided by Angular Material.
 */
export interface SnackEntry {
  message: string;
  config?: MatSnackBarConfig;
}
