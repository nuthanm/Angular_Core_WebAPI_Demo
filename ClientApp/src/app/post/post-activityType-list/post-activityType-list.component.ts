import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/shared/login.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PostActivity } from 'src/app/shared/post-activity.model';
import { Employee } from 'src/app/shared/employee.model';
import { PostActivityService } from 'src/app/shared/post-activity.service';
import { EmployeeService } from 'src/app/shared/employee.service';
import { Post } from 'src/app/shared/post.model';

@Component({
  selector: 'app-post-activityType-list',
  templateUrl: './post-activityType-list.component.html'  
})
export class PostActivityTypeListComponent implements OnInit {


  public postActivities: PostActivity;
  public posts: Post[] = [];
  public employee: Employee;  
  public errorMessage;
  private token: string;
  private employeeId;
  public postActivityType: string;
  public postId; number;
  public currentYear = new Date();
  constructor(private _router: Router, private _loginService: LoginService, private _postActivityService: PostActivityService, private _activatedRoute: ActivatedRoute, private _employeeService: EmployeeService) { }

  ngOnInit() {

    this.postActivityType = (this._activatedRoute.snapshot.paramMap.get('type'));
    this.employee = new Employee();

    if (localStorage.getItem("JwtTokenForAuthenticate")) {
      let tokenWithEmployeeId = JSON.parse(localStorage.getItem("JwtTokenForAuthenticate"));
      this.token = tokenWithEmployeeId.Token;
      this.employeeId = tokenWithEmployeeId.EmployeeId;
    }
    else {
      console.log('exits');
      this._router.navigate(['']);
    }
    if (this.employeeId > 0) {

      this._employeeService.GetEmployeeDetail(this.employeeId, this.token)
        .subscribe(response => {
          this.employee = response;
        }, error => {
          this.errorMessage = error
          console.log(error)
        });

      this._postActivityService.GetPostActivitiesByType(this.postActivityType, this.employeeId, this.token)
        .subscribe(activityData => this.posts = activityData);

    }
  }

  logout() {
    this._loginService.logout();
    this._router.navigate(['']);
  }
}
