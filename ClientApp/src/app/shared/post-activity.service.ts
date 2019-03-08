import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { PostActivity } from './post-activity.model';
import { UserActivities } from './user-activity.model';
import { Post } from './post.model';


@Injectable({
  providedIn: 'root'
})
export class PostActivityService {

  constructor(private http: HttpClient) { }

  private _baseUrl: string = environment.ApiBaseUrl;

  //GET post activities like isLike,isBookmark by logged user
  //This will call in Dashboard @ activity section
  GetPostActivitiesById(employeeId: number, token: string): Observable<PostActivity> {
    return this.http.get<PostActivity>(this._baseUrl + "api/postactivity/" + employeeId, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + token,
        "Content-Type": "application/json"
      })
    }).pipe(catchError(this.errorHandler));
  }

  //GET Posts based on bookmark/like/answer/open
  GetPostActivitiesByType(type: string, employeeId: number, token: string): Observable<Post[]> {
    return this.http.get<Post[]>(this._baseUrl + "api/postactivity/" + employeeId + "/" + type, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + token,
        "Content-Type": "application/json"
      })
    }).pipe(catchError(this.errorHandler));
  }

  CheckExists(postId: number, employeeId: number, token: string): Observable<PostActivity> {
    return this.http.get<PostActivity>(this._baseUrl + "api/postactivity/" + postId + "/" + employeeId, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }

  AddActivityType(type: string, postactivity: PostActivity, token: string) {
    return this.http.post<any>(this._baseUrl + "api/postactivity/" + type, postactivity, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }

  UpdateActivityType(postActivityId: number, postactivity: PostActivity, token: string) {
    return this.http.put<any>(this._baseUrl + "api/postactivity/" + postActivityId, postactivity, {
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
