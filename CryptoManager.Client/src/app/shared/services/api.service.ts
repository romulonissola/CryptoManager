import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { JwtService } from './jwt.service';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ApiService {
  constructor(
    private http: HttpClient,
    private jwtService: JwtService
  ) {}
  
  private GetHttpHeaders(): HttpHeaders{
    return new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': `Bearer ${this.jwtService.getToken()}`
    });
  }
  

  private formatErrors(error: any) {
    return ErrorObservable.create(error);
  }

  get(path: string, params: HttpParams = new HttpParams()): Observable<any> {
    return this.http.get(
      `${environment.api.baseUrl}${path}`,
      {headers: this.GetHttpHeaders(), params}
    ).pipe(catchError(this.formatErrors));
  }

  put(path: string, body: Object = {}): Observable<any> {
    return this.http.put(
      `${environment.api.baseUrl}${path}`,
      JSON.stringify(body),
      {headers: this.GetHttpHeaders()}
    ).pipe(catchError(this.formatErrors));
  }

  post(path: string, body: Object = {}, params: HttpParams = new HttpParams()): Observable<any> {
    return this.http.post(
      `${environment.api.baseUrl}${path}`,
      JSON.stringify(body),
      {headers: this.GetHttpHeaders(), params: params}
    ).pipe(catchError(this.formatErrors));
  }

  delete(path): Observable<any> {
    return this.http.delete(
      `${environment.api.baseUrl}${path}`,
      {headers: this.GetHttpHeaders()}
    ).pipe(catchError(this.formatErrors));
  }
}