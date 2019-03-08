import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Post } from 'src/app/shared/post.model';
import { Employee } from 'src/app/shared/employee.model';
import { PostService } from 'src/app/shared/post.service';
import { EmployeeService } from 'src/app/shared/employee.service';
import { PostActivity } from 'src/app/shared/post-activity.model';
import { PostActivityService } from 'src/app/shared/post-activity.service';
import { LoginService } from 'src/app/shared/login.service';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html'  
})
export class PostDetailComponent implements OnInit {

  private token: string;
  private postId; number;
  private employeeId: number;
  private postActivityId: number;

  public errorMessage: string;  
  
  public isBookmark: boolean;
  public isLike: boolean;
  public BookMarkResult: boolean;

  public post = new Post();
  public employee = new Employee();
  public postActivity = new PostActivity();

  constructor(private _router: Router, private _loginService: LoginService, private _activatedRoute: ActivatedRoute, private _postActivity: PostActivityService, private _postService: PostService, private _employeeService: EmployeeService) { }

  ngOnInit() {

    this.postId = (this._activatedRoute.snapshot.paramMap.get('id'));

    if (localStorage.getItem("JwtTokenForAuthenticate")) {
      let tokenWithEmployeeId = JSON.parse(localStorage.getItem("JwtTokenForAuthenticate"));

      this.token = tokenWithEmployeeId.Token;
      this.employeeId = tokenWithEmployeeId.EmployeeId;

      this._employeeService.GetEmployeeDetail(this.employeeId, this.token)
        .subscribe(response => {
          this.employee = response;
        }, error => {
          this.errorMessage = error
          console.log(error)
        });

      this._postService.GetPostDetail(this.postId, this.token)
        .subscribe(postData => {
          console.log(postData);
          var postObject= postData[0];
          this.post = postObject;
          this.BookMarkResult = this.post.IsBookmark;
          this.isLike = this.post.IsLike;
        });
    }
    else {
      this._router.navigate(['']);
    }
  }

  BookmarkThisPost() {
    this.postActivity.EmployeeId = this.employeeId;
    this.postActivity.PostId = this.postId;

    this._postActivity.CheckExists(this.postActivity.PostId, this.postActivity.EmployeeId, this.token).subscribe(postActivityDetails => {
      this.postActivity.ModifiedDate = new Date();
      this.postActivity.EmployeeId = this.employeeId;
      this.postActivity.PostId = this.postId;
      if (postActivityDetails != null) {
        if (postActivityDetails.PostActivityId > 0) {
          if (postActivityDetails.IsBookmark) {
            this.postActivity.IsBookmark = false;
          }
          else {
            this.postActivity.IsBookmark = true;
          }
          this.postActivity.PostActivityId = postActivityDetails.PostActivityId;
          this.postActivity.IsLike = postActivityDetails.IsLike;
          this._postActivity.UpdateActivityType(postActivityDetails.PostActivityId, this.postActivity, this.token).subscribe(response => {
            if (response == "Success") {
              this.BookMarkResult = this.postActivity.IsBookmark;
            }
            console.log(this.BookMarkResult);
          }, error => {
              this.errorMessage = error
              console.log(error)
            });
        }
      }
      else {
        this.postActivity.IsBookmark = true;
        this._postActivity.AddActivityType('Bookmark', this.postActivity, this.token).subscribe(response => {
          if (response == "Success") {
            this.BookMarkResult = this.postActivity.IsBookmark;
          }
        }, error => {
          this.errorMessage = error
          console.log(error)
        });
      }
    }), error => {
        this.errorMessage = error
        console.log(error)
      };
  }

  LikeThisPost() {
    this.postActivity.EmployeeId = this.employeeId;
    this.postActivity.PostId = this.postId;

    this._postActivity.CheckExists(this.postActivity.PostId, this.postActivity.EmployeeId, this.token).subscribe(postActivityDetails => {
      this.postActivity.ModifiedDate = new Date();
      this.postActivity.EmployeeId = this.employeeId;
      this.postActivity.PostId = this.postId;
      if (postActivityDetails.PostActivityId > 0) {
        if (postActivityDetails.IsLike) {
          this.postActivity.IsLike = false;
        }
        else {
          this.postActivity.IsLike = true;
        }
        this.postActivity.PostActivityId = postActivityDetails.PostActivityId;
        this.postActivity.IsBookmark = postActivityDetails.IsBookmark;
        this._postActivity.UpdateActivityType(postActivityDetails.PostActivityId, this.postActivity, this.token).subscribe(response => {
          if (response == "Success") {
            console.log('this.postActivity.IsLike ' + this.postActivity.IsLike);
            this.isLike = this.postActivity.IsLike;
            console.log('this.isLike ' + this.isLike);
          }
        }, error => {
          this.errorMessage = error
          console.log(error)
        });
      }
      else {
        this.postActivity.IsLike = true;
        this._postActivity.AddActivityType('Like', this.postActivity, this.token).subscribe(response => {
          if (response == "Success") {
            console.log('this.postActivity.IsLike ' + this.postActivity.IsLike);
            this.isLike = this.postActivity.IsLike;
            console.log('this.isLike ' + this.isLike);
          }
        }, error => {
          this.errorMessage = error
          console.log(error)
        });
      }
    }), error => {
        this.errorMessage = error
        console.log(error)
      };
  }

  logout() {
    this._loginService.logout();
    this._router.navigate(['']);
  }
}
