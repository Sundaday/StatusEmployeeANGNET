import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environments';
import { Observable } from 'rxjs';
import { ResponseApi } from '../Interfaces/response-api';
import { Employee } from '../Interfaces/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private endpoint:string = environment.endpoint;
  private myApiUrl:string = this.endpoint + "api/employee";

  constructor(private http:HttpClient) { }

  getListEmp():Observable<ResponseApi>{
    return this.http.get<ResponseApi>(this.myApiUrl)
  }

  addEmp(request:Employee):Observable<ResponseApi>{
    return this.http.post<ResponseApi>(this.myApiUrl, request)
  }

  putEmp(request:Employee):Observable<ResponseApi>{
    return this.http.put<ResponseApi>(this.myApiUrl, request)
  }

  deleteEmp(id:number):Observable<ResponseApi>{
    return this.http.delete<ResponseApi>(`${this.myApiUrl}/${id}`)
  }
}
