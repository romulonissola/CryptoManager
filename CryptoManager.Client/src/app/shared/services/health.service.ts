import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { Health } from '../models/health.model';

import { ApiService } from './api.service';

@Injectable()
export class HealthService {

  public serviceURL = "/health";
  
  constructor (private http: HttpClient) {}

  private GetHttpHeaders(): HttpHeaders{
    return new HttpHeaders({
      'Content-Type':  'application/json'
    });
  }
  
  getAll(): Observable<any>{
    return this.http.get(
      `${environment.api.baseUrl}${this.serviceURL}`,
      {headers: this.GetHttpHeaders()}
    ).pipe();
  }
}