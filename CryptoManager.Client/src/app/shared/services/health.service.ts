import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
@Injectable()
export class HealthService {
  public serviceURL = "/health";

  constructor(private http: HttpClient) {}

  private GetHttpHeaders(): HttpHeaders {
    return new HttpHeaders({
      "Content-Type": "application/json",
    });
  }

  getAllForCryptoManager(): Observable<any> {
    return this.http
      .get(`${environment.api.baseUrl}${this.serviceURL}`, {
        headers: this.GetHttpHeaders(),
      })
      .pipe();
  }

  getAllForRoboTrader(): Observable<any> {
    return this.http
      .get(`${environment.api.roboTraderBaseUrl}${this.serviceURL}`, {
        headers: this.GetHttpHeaders(),
      })
      .pipe();
  }

  ping(): Observable<any> {
    return this.http
      .get(`${environment.api.baseUrl}${this.serviceURL}/ping`, {
        headers: this.GetHttpHeaders(),
      })
      .pipe();
  }
}
