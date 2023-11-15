import { Injectable } from '@angular/core';
import { Observable ,  BehaviorSubject ,  ReplaySubject } from 'rxjs';

import { ApiService } from './api.service';
import { ApiType, Exchange } from '../models/index';
import { distinctUntilChanged, map } from 'rxjs/operators';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class ExchangeService {

  public serviceURL = "/exchange";

  constructor (private apiService: ApiService) {}

  getAll() : Observable<any> {
    return this.apiService.get(this.serviceURL, null, ApiType.CryptoManagerServerApi);
  }

  get(id:string) : Observable<any> {
    return this.apiService.get(this.getUrl(id), null, ApiType.CryptoManagerServerApi);
  }

  add(exchange:Exchange): Observable<Exchange>{
    return this.apiService.post(this.serviceURL, exchange, null, ApiType.CryptoManagerServerApi);
  }

  update(exchange:Exchange){
    return this.apiService.put(this.serviceURL, exchange, ApiType.CryptoManagerServerApi);
  }

  delete(id:string){
    return this.apiService.delete(this.getUrl(id), ApiType.CryptoManagerServerApi);
  }

  private getUrl(id){
    return this.serviceURL + "/" + id;
  }
}