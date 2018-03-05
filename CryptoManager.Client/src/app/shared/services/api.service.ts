import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { JwtService } from './jwt.service';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError } from 'rxjs/operators/catchError';

@Injectable()
export class ApiService {
  constructor(
    private http: HttpClient,
    private jwtService: JwtService
  ) {}
  
  private httpHeaders = new HttpHeaders({
      'Content-Type':  'application/json',
      'Authorization': `Bearer ${this.jwtService.getToken()}`
    });
  

  private formatErrors(error: any) {
    return new ErrorObservable(error.json());
  }

  get(path: string, params: HttpParams = new HttpParams()): Observable<any> {
    return this.http.get(
      `${environment.api.baseUrl}${path}`,
      {headers: this.httpHeaders, params}
    ).pipe(catchError(this.formatErrors));
  }

  put(path: string, body: Object = {}): Observable<any> {
    return this.http.put(
      `${environment.api.baseUrl}${path}`,
      JSON.stringify(body),
      {headers: this.httpHeaders}
    ).pipe(catchError(this.formatErrors));
  }

  post(path: string, body: Object = {}, params: HttpParams = new HttpParams()): Observable<any> {
    return this.http.post(
      `${environment.api.baseUrl}${path}`,
      JSON.stringify(body),
      {headers: this.httpHeaders, params: params}
    ).pipe(catchError(this.formatErrors));
  }

  delete(path): Observable<any> {
    return this.http.delete(
      `${environment.api.baseUrl}${path}`,
      {headers: this.httpHeaders}
    ).pipe(catchError(this.formatErrors));
  }
}