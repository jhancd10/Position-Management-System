import { Component, computed, inject } from '@angular/core';
import { DepartmentsStateService } from '../../services/departments-state.service';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { StateStatusEnum } from '../../../shared/enums/state-status.enum';
import { Department } from '../../interfaces/department.interface';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatTooltipModule} from '@angular/material/tooltip';
import { DepartmentFormDialogComponent } from '../../ui/department-form-dialog/department-form-dialog.component';
import { ConfirmDialogComponent } from '../../../shared/ui/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-departments-list',
  standalone: true,
  imports: [
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatCardModule,
    MatTooltipModule,
    MatDialogModule,
  ],
  templateUrl: './departments-list.component.html',
  styleUrl: './departments-list.component.scss'
})
export default class DepartmentsListComponent {

  private dialog = inject(MatDialog);
  private departmentsStateService = inject(DepartmentsStateService);


  public displayedColumns: string[] = ['name', 'positions', 'actions'];
  
  public stateStatusEnum = StateStatusEnum;

  public departments = computed(
    () => this.departmentsStateService.state().data as Department[]
  );

  public status = computed(() => this.departmentsStateService.state().status);

  
  public openDepartmentDialog(department?: Department) {

    const dialogRef = this.dialog.open(
      DepartmentFormDialogComponent, 
      {
        data: {
          department,
          isEdit: !!department
        }
    });

    dialogRef.afterClosed().subscribe((result: Department) => {
      if (result) {
        if (department) this.departmentsStateService.state.update(result);
        else this.departmentsStateService.state.create(result);
      }
    });
  }

  public confirmDelete(department: Department) {

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Confirm Delete',
        message: `Are you sure you want to delete the department "${department.name}"?`,
        confirmText: 'Delete',
        cancelText: 'Cancel'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) this.departmentsStateService.state.delete(department);
    });
  }
}
