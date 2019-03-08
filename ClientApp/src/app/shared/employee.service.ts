import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Employee } from './employee.model';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  
  constructor(private http: HttpClient) { }

  private _baseUrl: string = environment.ApiBaseUrl;
  
  //GET Particular Employee
  GetEmployeeDetail(employeeId: number,token:string): Observable<Employee> {    
    return this.http.get<Employee>(this._baseUrl + "api/Employee/" + employeeId,{      
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })}).pipe(catchError(this.errorHandler));
  }

  //POST
  AddEmployee(employee: Employee,token:string) {  
    return this.http.post<Employee>(this._baseUrl + "api/Employee", employee,{      
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })})
      .pipe(catchError(this.errorHandler));

  }

  //PUT
  UpdateEmployee(employee: Employee, employeeId: number,token:string) {
    return this.http.put<Employee>(this._baseUrl + "api/Employee/" + employeeId, employee,{      
      headers: new HttpHeaders({
        'Authorization': 'Bearer ' + token,
        'Content-Type': 'application/json'
      })})
      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: HttpErrorResponse) {
    return throwError(error.message);
  }
}