import { Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environments';
import { Observable } from 'rxjs';
import { ResponseApi } from '../Interfaces/response-api';


@Injectable({
  providedIn: 'root'
})
export class DepartmentService {
  private endpoint:string = environment.endpoint;
  private myApiUrl:string = this.endpoint + "api/department";

  constructor(private http:HttpClient) { }

  getListDep():Observable<ResponseApi>{
    return this.http.get<ResponseApi>(this.myApiUrl);
  }
}
