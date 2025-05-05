import { inject, Injectable } from '@angular/core';
import {
  MatSnackBar,
  MatSnackBarConfig,
  MatSnackBarRef,
} from '@angular/material/snack-bar';
import { SnackEntry } from '../interfaces/snack-entry.interface';

@Injectable({ providedIn: 'root' })
export class UiService {

  private snackBar = inject(MatSnackBar);

  private queue: SnackEntry[] = [];
  private activeRef: MatSnackBarRef<any> | null = null;

  showError(message: string, config?: MatSnackBarConfig) {
    this.enqueue({ message, config });
  }

  private enqueue(entry: SnackEntry) {
    
    this.queue.push(entry);
    if (!this.activeRef) {
      this.showNext();
    }
  }

  private showNext() {

    const entry = this.queue.shift();

    if (!entry) {
      this.activeRef = null;
      return;
    }

    const cfg: MatSnackBarConfig = {
      duration: 3000,
      verticalPosition: 'bottom',
      horizontalPosition: 'center',
      ...entry.config,
    };

    this.activeRef = this.snackBar.open(entry.message, 'Close', cfg);

    this.activeRef.afterDismissed().subscribe(() => {
      this.activeRef = null;
      this.showNext();
    });
  }
}
