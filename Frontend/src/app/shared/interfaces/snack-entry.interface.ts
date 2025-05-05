import { MatSnackBarConfig } from '@angular/material/snack-bar';

export interface SnackEntry {
  message: string;
  config?: MatSnackBarConfig;
}
