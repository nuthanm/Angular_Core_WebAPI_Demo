import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { Employee } from '../shared/employee.model';
import { Router } from '@angular/router';
import { CategoryService } from '../shared/category.service';
import { Category } from '../shared/category.model';
import { PostService } from '../shared/post.service';
import { NgForm } from '@angular/forms';
import { Post } from '../shared/post.model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styles: [
    `
    .mt-50{
      margin-top:50px !important;
    }
    `
  ]
})
export class PostComponent implements OnInit {

  public categories: Category[] = [];  
  public post = new Post();
  public posts : Post[]=[]; 
  public organization = environment.OraganizationCode;
  public organizationURL = environment.OraganizationUrl;
  public errorMessage;
  private token: string;
  public categoryStatus = true;  
  private employeeId;
  @ViewChild('selectCategory') select: ElementRef;
  constructor(private _router: Router, private _categoryService: CategoryService, private _postService: PostService) { }

  ngOnInit() {
    if (localStorage.getItem("JwtTokenForAuthenticate")) {
      let tokenWithEmployeeId = JSON.parse(localStorage.getItem("JwtTokenForAuthenticate"));
      this.token = tokenWithEmployeeId.Token;
      this.employeeId = tokenWithEmployeeId.EmployeeId;

      this._categoryService.getCategory(this.token)
        .subscribe(catData => this.categories = catData);
    }
    else {
      this._router.navigate(['']);
    }

  }
  clearAllItems(postForm: NgForm) {
    postForm.form.reset();
  }



  goToPosts() {
    this._router.navigate(['/post-list'])
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
}
