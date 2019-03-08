import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/shared/employee.model';
import { EmployeeService } from 'src/app/shared/employee.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { LoginService } from 'src/app/shared/login.service';

@Component({
  selector: 'app-employeedetail',
  templateUrl: './employeedetail.component.html'

})
export class EmployeedetailComponent implements OnInit {

  public employee=new  Employee();
  public employeeDetail = new Employee();
  public employeeId;
  public ErrorMessage;
  private token;
  public SuccessMessage: string = '';
  public currentYear = new Date();
  constructor(private _employeeService: EmployeeService, private _router: Router, private _activatedRoute: ActivatedRoute,private _loginService: LoginService) { }

  ngOnInit() {

    if (localStorage.getItem("JwtTokenForAuthenticate")) {
      let tokenWithEmployeeId = JSON.parse(localStorage.getItem("JwtTokenForAuthenticate"));
      this.token = tokenWithEmployeeId.Token;
      this.employeeId = tokenWithEmployeeId.EmployeeId;
      console.log("Token is : " + this.token)
    }
    else {
      this._router.navigate(['']);
    }

    if (this.employeeId > 0) {
      this._employeeService.GetEmployeeDetail(this.employeeId, this.token)
        .subscribe(response => {
          this.employee = response;
        }, error => {
          this.ErrorMessage = error
          console.log(error)
        });
    }
  }

  onSubmit(employeeForm: NgForm) {

    this.employee.FullName = this.employee.FirstName + ' ' + this.employee.LastName;
    this.employee.CreatedDate=new Date();
    this._employeeService.UpdateEmployee(this.employee, this.employeeId, this.token)        
      .subscribe
      (
        (data: Employee) => {
          this.SuccessMessage = data.message;
          console.log(this.SuccessMessage);
        },
        error => {
          console.log('Error!!!', error)
        });    
  }

  redirectToDashboard() {
    this._router.navigate(['/dashboard']);
  }

  logout() {
    this._loginService.logout();  
    this._router.navigate(['']);
  }
}


