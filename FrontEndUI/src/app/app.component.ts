import { Component, OnInit, AfterViewInit, ViewChild } from '@angular/core';

import { Employee } from './Interfaces/employee';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { EmployeeService } from './Services/employee.service';

import { DialogAddEditComponent } from './Dialogs/dialog-add-edit/dialog-add-edit.component';
import { DialogDeleteComponent } from './Dialogs/dialog-delete/dialog-delete.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['FullName', 'Department', 'Salary', 'HireDate', 'Actions'];
  dataEmployee = new MatTableDataSource<Employee>();

  //@ViewChild(MatPaginator, {static: true}) paginator: MatPaginator = new MatPaginator(new MatPaginatorIntl(), ChangeDetectorRef.prototype);
  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(
    private _snackBar: MatSnackBar,
    private _employeeService: EmployeeService,
    private dialog: MatDialog
  ) { }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataEmployee.filter = filterValue.trim().toLowerCase();
  }

  showEmployees() {
    this._employeeService.getListEmp().subscribe({
      next: (data) => {
        if (data.status) {
          this.dataEmployee.data = data.value
        }
      },
      error: (e) => { }
    })
  }

  ngOnInit(): void {
    this.showEmployees();
  }

  ngAfterViewInit(): void {
    this.dataEmployee.paginator = this.paginator;
  }

  addNewEmployee() {
    this.dialog.open(DialogAddEditComponent, {
      disableClose: true,
      width: "350px"
    }).afterClosed().subscribe(result => {
      if (result === "Created") {
        this.showEmployees();
      }
    })
  }

  editEmployee(employee: Employee) {
    this.dialog.open(DialogAddEditComponent, {
      disableClose: true,
      data: employee,
      width: "350px"
    }).afterClosed().subscribe(result => {
      if (result === "Edited") {
        this.showEmployees();
      }
    })
  }

  showAlert(msg: string, title: string) {
    this._snackBar.open(msg, title, {
      horizontalPosition: "end",
      verticalPosition: "top",
      duration: 3000
    })
  }

  deleteEmployee(employee: Employee) {
    this.dialog.open(DialogDeleteComponent, {
      disableClose: true,
      data: employee
    }).afterClosed().subscribe(result => {
      if (result === "Delete") {
        this._employeeService.deleteEmp(employee.idEmployee).subscribe({
          next: (data) => {
            if (data.status) {
              this.showAlert("Employee deleted", "Success")
              this.showEmployees();
            } else {
              this.showAlert("Could not delete employee", "Error")
            }
          },
          error: (e) => { }
        })
      }
    })
  }
}
