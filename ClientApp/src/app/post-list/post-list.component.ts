import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import {Router,ActivatedRoute} from '@angular/router';
import { PostService } from '../shared/post.service';
import { Post } from '../shared/post.model';
import { Employee } from '../shared/employee.model';
import { EmployeeService } from '../shared/employee.service';
import { LoginService } from '../shared/login.service';
import { CategoryService } from '../shared/category.service';
import { NgForm } from '@angular/forms';
import { Category } from '../shared/category.model';

@Component({
  selector: 'app-post-list',
  templateUrl: './post-list.component.html'  
})
export class PostListComponent implements OnInit {  
  public post = new Post();
  public posts : Post[]=[];  
  public errorMessage;
  private token: string;
  public categoryStatus = true;  
  private employeeId;
  public employee = new Employee();
  public categories: Category[] = []; 
  public currentYear = new Date();
  @ViewChild('selectCategory') select: ElementRef;
  constructor(private _router : Router,private _categoryService: CategoryService,private _postService : PostService,private _employeeService : EmployeeService,private _loginService:LoginService) { }

  ngOnInit() {
    if (localStorage.getItem("JwtTokenForAuthenticate")) {
      let tokenWithEmployeeId = JSON.parse(localStorage.getItem("JwtTokenForAuthenticate"));
      this.token = tokenWithEmployeeId.Token;
      this.employeeId = tokenWithEmployeeId.EmployeeId;
      
      this._employeeService.GetEmployeeDetail(this.employeeId,this.token)
      .subscribe(response => {
        this.employee = response;
      }, error => {
        this.errorMessage = error
        console.log(error)
      });

      this._categoryService.getCategory(this.token)
      .subscribe(catData => this.categories = catData);

      this._postService.GetAllPosts(this.employeeId,this.token)
        .subscribe(postData => this.posts = postData);
    }
    else {
      this._router.navigate(['']);
    }
  }

  clearAllItems(postForm: NgForm) {
    postForm.form.reset();
  }

  validateCategory(categoryValue) {
    if (categoryValue === "Select Category") {
      this.categoryStatus = true;
    }
    else {
      this.categoryStatus = false;
    }
  }

  postAQuestion(postForm: NgForm) {
    if (postForm.form.valid) {
      this.post.CategoryId = this.select.nativeElement.value;
      this.post.EmployeeId = this.employeeId;
      this.post.CreatedDate=new Date();
      this._postService.AddPost(this.post, this.token).subscribe(response => {
        console.log(response);
        this._postService.GetAllPosts(this.employeeId,this.token)
        .subscribe(postData => this.posts = postData);        
        postForm.form.reset();
      }, error => {
        this.errorMessage = error
        console.log(error)
      });
    }
  }

  EditPost(postId : number)
  {
        this._postService.GetPostDetail(postId,this.employeeId,this.token) .subscribe(response => {
          this.post =response;
        },error => {
          this.errorMessage =error;
        });
  }

  logout()
  {
    this._loginService.logout();  
    this._router.navigate(['']);
  }
}
