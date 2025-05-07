/**
 * Component representing a confirmation dialog.
 * 
 * This dialog is used to display a confirmation message to the user
 * and allows them to confirm or cancel an action. It uses Angular Material's
 * dialog module for its implementation.
 * 
 * @selector app-confirm-dialog
 * @standalone true
 * @imports MatDialogModule
 * 
 * @templateUrl ./confirm-dialog.component.html
 * @styleUrl ./confirm-dialog.component.scss
 * 
 * @class ConfirmDialogComponent
 * 
 * @constructor
 * @param dialogRef - Reference to the dialog opened, used to close the dialog and return a result.
 * @param data - Data passed to the dialog, containing the confirmation message and other relevant information.
 */
import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialogData } from '../../interfaces/confirm-dialog.interface';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [
    MatDialogModule
  ],
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.scss'
})
export class ConfirmDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: ConfirmDialogData
  ) { }
}
