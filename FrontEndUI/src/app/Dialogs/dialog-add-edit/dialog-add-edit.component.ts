import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MAT_DATE_FORMATS } from '@angular/material/core';
import { Department } from 'src/app/Interfaces/department';
import { Employee } from 'src/app/Interfaces/employee';
import { DepartmentService } from 'src/app/Services/department.service';
import { EmployeeService } from 'src/app/Services/employee.service';
import * as moment from 'moment';
import { ThisReceiver } from '@angular/compiler';

export const MY_DATE_FORMATS = {
  parse: {
    dateInput: 'DD/MM/YYYY'
  },
  display: {
    dateInput: 'DD/MM/YYYY',
    monthYearLabel: 'MMMM YYYY',
    dataA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY'
  }
}

@Component({
  selector: 'app-dialog-add-edit',
  templateUrl: './dialog-add-edit.component.html',
  styleUrls: ['./dialog-add-edit.component.css'],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: MY_DATE_FORMATS }
  ]
})
export class DialogAddEditComponent implements OnInit {
  formEmployee: FormGroup;
  action: string = "Add";
  actionButton: string = "Save";
  listDepartment: Department[] = [];

  constructor(
    private dialogReference: MatDialogRef<DialogAddEditComponent>,
    @Inject(MAT_DIALOG_DATA) public employeeData: Employee,
    private fb: FormBuilder,
    private _snackBar: MatSnackBar,
    private _departmentService: DepartmentService,
    private _employeeService: EmployeeService
  ) {
    this.formEmployee = this.fb.group({
      fullName: ["", Validators.required],
      idDepartment: ["", Validators.required],
      salary: ["", Validators.required],
      hireDate: ["", Validators.required],
    });
    this._departmentService.getListDep().subscribe({
      next: (data) => {
        if (data.status) {
          this.listDepartment = data.value;
        }
      },
      error: (e) => { }
    });
  }

  ngOnInit(): void {
    if (this.employeeData) {
      this.formEmployee.patchValue({
        fullName: this.employeeData.fullName,
        idDepartment: this.employeeData.idDepartment,
        salary: this.employeeData.salary,
        hireDate: moment(this.employeeData.hireDate, 'DD/MM/YYYY')
      })
      this.action = "Edit";
      this.actionButton = "Update";
    }
  }

  showAlert(msg: string, title: string) {
    this._snackBar.open(msg, title, {
      horizontalPosition: "end",
      verticalPosition: "top",
      duration: 3000
    })
  }

  addEditEmployee() {
    const model: Employee = {
      idEmployee: this.employeeData == null ? 0 : this.employeeData.idEmployee,
      fullName: this.formEmployee.value.fullName,
      idDepartment: this.formEmployee.value.idDepartment,
      salary: this.formEmployee.value.salary,
      hireDate: moment(this.formEmployee.value.hireDate).format('DD/MM/YYYY')
    }
    if (this.employeeData == null) {
      this._employeeService.addEmp(model).subscribe({
        next: (data) => {
          if (data.status) {
            this.showAlert('Employee created', 'Success');
            this.dialogReference.close('Created');
          } else {
            this.showAlert('Could not create employee', 'Error')
          }
        },
        error: (e) => { }
      })
    } else {
      this._employeeService.putEmp(model).subscribe({
        next: (data) => {
          if (data.status) {
            this.showAlert('Employee updated', 'Success');
            this.dialogReference.close('Updated');
          } else {
            this.showAlert('Could not edit employee', 'Error')
          }
        },
        error: (e) => { }
      })
    }

  }
}
