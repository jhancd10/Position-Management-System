import { inject, Injectable } from '@angular/core';
import {
  MatSnackBar,
  MatSnackBarConfig,
  MatSnackBarRef,
} from '@angular/material/snack-bar';
import { SnackEntry } from '../interfaces/snack-entry.interface';

@Injectable({ providedIn: 'root' })
export class UiService {
  
  /**
   * Angular Material's MatSnackBar instance, used to display snack bar notifications.
   */
  private snackBar = inject(MatSnackBar);

  /**
   * Queue to hold snack bar entries. Each entry represents a message and its configuration.
   */
  private queue: SnackEntry[] = [];

  /**
   * Reference to the currently active snack bar. If null, no snack bar is currently displayed.
   */
  private activeRef: MatSnackBarRef<any> | null = null;

  /**
   * Displays an error message in a snack bar. The message is added to the queue
   * and will be displayed sequentially after any currently active snack bar is dismissed.
   *
   * @param message - The error message to display.
   * @param config - Optional configuration for the snack bar.
   */
  showError(message: string, config?: MatSnackBarConfig) {
    this.enqueue({ message, config });
  }

  /**
   * Adds a snack bar entry to the queue. If no snack bar is currently active,
   * it triggers the display of the next snack bar in the queue.
   *
   * @param entry - The snack bar entry to enqueue, containing the message and optional configuration.
   */
  private enqueue(entry: SnackEntry) {
    this.queue.push(entry);
    if (!this.activeRef) {
      this.showNext();
    }
  }

  /**
   * Displays the next snack bar in the queue. If the queue is empty, it sets the active reference to null.
   * This method ensures that snack bars are displayed one at a time, in the order they were added to the queue.
   *
   * @remarks
   * The method configures the snack bar with default settings, which can be overridden
   * by the configuration provided in the snack bar entry.
   *
   * @private
   */
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

    // Subscribe to the afterDismissed event to trigger the next snack bar in the queue.
    this.activeRef.afterDismissed().subscribe(() => {
      this.activeRef = null;
      this.showNext();
    });
  }
}
