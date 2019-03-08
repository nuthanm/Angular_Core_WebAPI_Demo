import { Component, OnInit } from '@angular/core';
import { Employee } from '../shared/employee.model';
import { Router, ActivatedRoute } from '@angular/router';
import { PostActivityService } from 'src/app/shared/post-activity.service';
import { EmployeeService } from '../shared/employee.service';
import { PostActivity } from '../shared/post-activity.model';
import { LoginService } from '../shared/login.service';
import { Chart } from 'chart.js';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styles: []
})
export class DashboardComponent implements OnInit {

  public postActivities: PostActivity;
  public employee: Employee;  
  public errorMessage;
  private token: string;// = environment.ApiJwtToken;
  private employeeId;
  private activityTypes: string[] = [];
  private activityCount: number[] = [];
  BarChart = [];
  public currentYear = new Date();

  constructor(private _router: Router, private _postActivityService: PostActivityService, private _activatedRoute: ActivatedRoute, private _employeeService: EmployeeService, private _loginService: LoginService) { }

  ngOnInit() {

    this.employee = new Employee();

    if (localStorage.getItem("JwtTokenForAuthenticate")) {
      let tokenWithEmployeeId = JSON.parse(localStorage.getItem("JwtTokenForAuthenticate"));
      this.token = tokenWithEmployeeId.Token;
      this.employeeId = tokenWithEmployeeId.EmployeeId;
    }
    else {
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

      this._postActivityService.GetPostActivitiesById(this.employeeId, this.token)
        .subscribe(activityData => {
          this.postActivities = activityData
          for (var i = 0; i < 4; i++) {
            this.activityTypes.push(this.postActivities[i].Activity);
            this.activityCount.push(this.postActivities[i].Count);
          }
          // Bar chart:
          this.BarChart = new Chart('barChart', {
            type: 'bar',
            data: {
              labels: this.activityTypes,
              datasets: [{           
                label: "# activity count",     
                data: this.activityCount,
                backgroundColor: [
                  'rgba(255, 99, 132, 0.2)',
                  'rgba(54, 162, 235, 0.2)',
                  'rgba(255, 206, 86, 0.2)',
                  'rgba(75, 192, 192, 0.2)'
                ],
                borderColor: [
                  'rgba(255,99,132,1)',
                  'rgba(54, 162, 235, 1)',
                  'rgba(255, 206, 86, 1)',
                  'rgba(75, 192, 192, 1)'
                ],
                borderWidth: 1
              }]
            },
            options: {
              title: {
                text: "User Activity",                
                display: true
              },
              scales: {
                yAxes: [{
                  ticks: {
                    beginAtZero: true
                  }
                }]
              }
            }
          });
        },
          error => this.errorMessage = error);
    }
  }


  onSelect(postActivity) {
    if (postActivity.Count > 0) {
      this._router.navigate(['/postactivitytype', postActivity.ActivityType])
    }
  }

  logout() {
    this._loginService.logout();
    this._router.navigate(['']);
  }

}

