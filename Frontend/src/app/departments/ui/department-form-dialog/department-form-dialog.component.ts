import { Component, Inject } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { DepartmentDialogData } from '../../interfaces/department-dialog-data.interface';
import { Department } from '../../interfaces/department.interface';

@Component({
  selector: 'app-department-form-dialog',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  templateUrl: './department-form-dialog.component.html',
  styleUrl: './department-form-dialog.component.scss'
})
export class DepartmentFormDialogComponent {

  departmentForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<DepartmentFormDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DepartmentDialogData
  ) {
    
    this.departmentForm = this.fb.group({
      name: [
        '', 
        [
          Validators.required, 
          Validators.minLength(3), 
          Validators.maxLength(100),
          Validators.pattern("[\\w\-*&'\"() ]+")
        ]
      ]
    });

    if (data.department) this.departmentForm.patchValue(data.department);
  }

  ngOnInit() {
    const elements = document.querySelectorAll('[aria-hidden]'); 
    elements.forEach(element => { element.removeAttribute('aria-hidden'); });
  }

  onSubmit() {
    if (this.departmentForm.valid)
      this.dialogRef.close(this.departmentForm.value as Department);
  }
}
