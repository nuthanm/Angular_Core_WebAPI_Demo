import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Post } from './post.model';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(private http: HttpClient) { }

  private _baseUrl: string = environment.ApiBaseUrl;

  //GET
  GetAllPosts(employeeId: number, token: string): Observable<Post[]> {
    return this.http.get<Post[]>(this._baseUrl + "api/post/eid/" + employeeId, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }

  //GET Particular post details
  GetPostDetail(postId: number, token: string): Observable<Post> {
    return this.http.get<Post>(this._baseUrl + "api/post/" + postId, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }

  //GET Posts based on isBookMark/isLiked
  GetPostsBasedOnType(type: string, token: string): Observable<Post> {
    return this.http.get<Post>(this._baseUrl + "api/post/" + type, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }

  //POST
  AddPost(post: Post, token: string) {
    return this.http.post<Post>(this._baseUrl + "api/post", post, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));

  }

  //PUT
  UpdateEmployee(post: Post, postId: number, token: string) {
    return this.http.put<Post>(this._baseUrl + "api/post/" + postId, post, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }


  //Delete
  DeleteEmployee(postId: number, token: string) {
    return this.http.delete<Post>(this._baseUrl + "api/post/" + postId, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }

  errorHandler(error: HttpErrorResponse) {
    return throwError(error.message);
  }
}
