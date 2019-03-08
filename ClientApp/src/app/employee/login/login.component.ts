import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Employee } from 'src/app/shared/employee.model';
import { NgForm, FormGroup } from '@angular/forms';
import { LoginService } from 'src/app/shared/login.service';
import { Login } from 'src/app/shared/login.model';

@Component({

  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {

  public appTitle = 'Employee Forum';
  public getYear = new Date();
  public currentYear = this.getYear.getFullYear() - 1 + " - " + this.getYear.getFullYear()
  public isRequired = true;
  public invalidLoginShowMessage;
  employee = new Employee();
  private invalidLogin;
  returnUrl: string;
  error = '';
  private login =new Login();
  constructor
    (
      private _router: Router, private _activatedRoute: ActivatedRoute, private _loginService: LoginService) { }

  ngOnInit() {
    this.invalidLoginShowMessage = "";
  }


  authenticate(loginForm: NgForm) {

    if (loginForm.form.valid) {
      this._loginService.authenticate(loginForm.controls.UserName.value, loginForm.controls.Password.value).subscribe(response => {
        let responseMessage = JSON.parse(localStorage.getItem("JwtTokenForAuthenticate"))
       if (responseMessage.message !=null)
       {
        this.invalidLoginShowMessage = "Username or Password is incorrect";
       }
       else
       {
        this.invalidLogin = false;
        this._router.navigate(["/dashboard"]);
       }
      }), error => {
        this.invalidLogin = true;
        console.log(error);
      };
    }
    else {
      this.invalidLoginShowMessage = "Please enter the required fields";
    }
  }

  gotoRegister() {
    this._router.navigate(['/register']);
  }
}
