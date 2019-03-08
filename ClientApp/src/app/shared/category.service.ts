import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import {Observable} from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Category } from './category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  private _baseUrl: string = environment.ApiBaseUrl;  

  //GET
  getCategory(token:string): Observable<Category[]>{
    return this.http.get<Category[]>(this._baseUrl+"api/category", {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    });
  }


  //POST
  AddPost(category: Category,token:string) {
    return this.http.post<Category>(this._baseUrl + "api/category",category, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));

  }

  //PUT
  UpdateEmployee( category: Category,categoryId: number,token:string)
  {
    return this.http.put<Category>(this._baseUrl + "api/category/"+ categoryId,category, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }  

  
  //Delete
  DeleteEmployee(categoryId: number,token :string)
  {
    return this.http.delete<Category>(this._baseUrl + "api/category/"+ categoryId, {
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })
    }).pipe(catchError(this.errorHandler));
  }  

  errorHandler(error : HttpErrorResponse)
  {
    return throwError(error.message);    
  }

}


