import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SidenavService {

  /**
   * Signal representing the current state of the sidenav.
   * - `true` indicates the sidenav is open.
   * - `false` indicates the sidenav is closed.
   */
  isOpen = signal(true);

  /**
   * Toggles the current state of the sidenav.
   * If the sidenav is open, it will be closed, and vice versa.
   */
  toggle() {
    this.isOpen.update((open) => !open);
  }

  /**
   * Opens the sidenav by setting its state to `true`.
   */
  open() {
    this.isOpen.set(true);
  }

  /**
   * Closes the sidenav by setting its state to `false`.
   */
  close() {
    this.isOpen.set(false);
  }
}
