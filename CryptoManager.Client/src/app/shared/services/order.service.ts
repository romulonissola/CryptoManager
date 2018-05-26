import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { ReplaySubject } from 'rxjs/ReplaySubject';

import { ApiService } from './api.service';
import { Order } from '../models/index';
import { distinctUntilChanged, map } from 'rxjs/operators';
import { HttpParams } from '@angular/common/http';
import { OrderDetail } from '../models/orderDetail.model';

@Injectable()
export class OrderService {

  public serviceURL = "/order";
  
  constructor (private apiService: ApiService) {}

  add(order:Order): Observable<Order>{
    return this.apiService.post(this.serviceURL, order);
  }

  getAllByLoggedUser(): Observable<any>{
    return this.apiService.get(`${this.serviceURL}/GetOrderDetailsByApplicationUser`);
  }
}