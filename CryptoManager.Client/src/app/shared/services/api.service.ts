import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment";
import { HttpHeaders, HttpClient, HttpParams } from "@angular/common/http";
import { Observable, from } from "rxjs";

import { JwtService } from "./jwt.service";
import { catchError } from "rxjs/operators";
import { ApiType } from "../models/api-type.enum";

@Injectable()
export class ApiService {
  constructor(private http: HttpClient, private jwtService: JwtService) {}

  private GetHttpHeaders(): HttpHeaders {
    return new HttpHeaders({
      "Content-Type": "application/json",
      Authorization: `Bearer ${this.jwtService.getToken()}`,
    });
  }

  resolveUrl(apiType: ApiType, path: string): string {
    switch (apiType) {
      case ApiType.CryptoManagerServerApi:
        return `${environment.api.baseUrl}${path}`;
      case ApiType.RoboTraderApi:
        return `${environment.api.roboTraderBaseUrl}${path}`;
      default:
        throw new Error("API type not implemented");
    }
  }

  private formatErrors(error: any) {
    return from(error);
  }

  get(
    path: string,
    params: HttpParams = new HttpParams(),
    apiType: ApiType
  ): Observable<any> {
    return this.http
      .get(this.resolveUrl(apiType, path), {
        headers: this.GetHttpHeaders(),
        params,
      })
      .pipe(catchError(this.formatErrors));
  }

  put(path: string, body: Object = {}, apiType: ApiType): Observable<any> {
    return this.http
      .put(this.resolveUrl(apiType, path), JSON.stringify(body), {
        headers: this.GetHttpHeaders(),
      })
      .pipe(catchError(this.formatErrors));
  }

  post(
    path: string,
    body: Object = {},
    params: HttpParams = new HttpParams(),
    apiType: ApiType
  ): Observable<any> {
    return this.http
      .post(this.resolveUrl(apiType, path), JSON.stringify(body), {
        headers: this.GetHttpHeaders(),
        params: params,
      })
      .pipe(catchError(this.formatErrors));
  }

  delete(path, apiType: ApiType): Observable<any> {
    return this.http
      .delete(this.resolveUrl(apiType, path), {
        headers: this.GetHttpHeaders(),
      })
      .pipe(catchError(this.formatErrors));
  }
}
