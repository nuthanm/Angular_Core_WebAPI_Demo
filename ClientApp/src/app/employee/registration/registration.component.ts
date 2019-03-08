import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Router } from '@angular/router';
import { Employee } from 'src/app/shared/employee.model';
import { EmployeeService } from 'src/app/shared/employee.service';
import { NgForm } from '@angular/forms';
import { LoginService } from 'src/app/shared/login.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {

  constructor(private _router: Router, private _registerAnEmployee: LoginService) { }
  gender: string = "M";
  employee = new Employee();
  public message: string;
  public isExists: boolean;
  @ViewChild('username') input: ElementRef;
  ngOnInit() {
  }

  redirectToLogin() {
    this._router.navigate(['../']);
  }

  clearAllItems(employeeDetails: NgForm) {
    employeeDetails.form.reset();
  }

  onSubmit(employeeDetails: NgForm) {
    if (employeeDetails.form.valid) {
      this.employee.FullName = this.employee.FirstName + ' ' + this.employee.LastName;
      this.employee.Gender = (this.employee.Gender === '0' ? 'F' : 'M');
      this.employee.CreatedDate = new Date();
      this.employee.LastLoggedInDate = new Date();

      this._registerAnEmployee.registerAnEmployee(this.employee)
        .subscribe
        (
          (data: Employee) => {
            console.log('Success', data.message)
            this.message = data.message;
            employeeDetails.form.reset();
          },
          error => console.log('Error!!!', error)
        )
    }
  }

  CheckUserName(userName: string) {
    if (userName.length > 0) {
      this._registerAnEmployee.CheckUserName(userName)
        .subscribe((data: any) => {
          if (data == null) {
            console.log("No response from serve");
          }
          this.isExists = data;
          console.log(data);
        },

          error => console.log('Error !!!', error)
        )
    }
  }
}
