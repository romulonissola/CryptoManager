import { Injectable } from '@angular/core';
import { Observable ,  BehaviorSubject ,  ReplaySubject } from 'rxjs';

import { ApiService } from './api.service';
import { Exchange } from '../models/index';
import { distinctUntilChanged, map } from 'rxjs/operators';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class ExchangeService {

  public serviceURL = "/exchange";

  constructor (private apiService: ApiService) {}

  getAll() : Observable<any> {
    return this.apiService.get(this.serviceURL);
  }

  get(id:string) : Observable<any> {
    return this.apiService.get(this.getUrl(id));
  }

  add(exchange:Exchange): Observable<Exchange>{
    return this.apiService.post(this.serviceURL, exchange);
  }

  update(exchange:Exchange){
    return this.apiService.put(this.serviceURL, exchange);
  }

  delete(id:string){
    return this.apiService.delete(this.getUrl(id));
  }

  private getUrl(id){
    return this.serviceURL + "/" + id;
  }
}