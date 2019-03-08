import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Login } from './login.model';
import { environment } from 'src/environments/environment';
import { map, catchError } from 'rxjs/operators';
import { throwError, Observable } from 'rxjs';
import { Employee } from './employee.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http: HttpClient) { }
  private _baseUrl: string = environment.ApiBaseUrl;
  private isExists: boolean;
  authenticate(username: string, password: string) {    
    return this.http.post<Login>(this._baseUrl + 'api/auth/login', { username, password })
      .pipe(
        map(userToken => {
          if (userToken) {
            localStorage.setItem('JwtTokenForAuthenticate', JSON.stringify(userToken));
          }
          else
          {
            return userToken.message;
          }
        })).pipe(catchError(this.errorHandler));
  }

  registerAnEmployee(employee :Employee)
  {
    return this.http.post<Employee>(this._baseUrl + "api/auth/register", employee,{      
      headers: new HttpHeaders({        
        'Content-Type': 'application/json'
      })})
      .pipe(catchError(this.errorHandler));
  }

  logout() {   
    localStorage.removeItem("JwtTokenForAuthenticate");
  }

  CheckUserName(userName: string) :Observable<any>
  {
    return this.http.get<Employee>(this._baseUrl + "api/auth/exists/"+ userName,{      
      headers: new HttpHeaders({     
        'Content-Type': 'text/plain'
      })}).pipe(catchError(this.errorHandler));
    
  }
  errorHandler(error: HttpErrorResponse) {
    return throwError(error.message);

  }
}
