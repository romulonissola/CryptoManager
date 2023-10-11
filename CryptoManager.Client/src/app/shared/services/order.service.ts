import { Injectable } from "@angular/core";
import { Observable, BehaviorSubject, ReplaySubject } from "rxjs";

import { ApiService } from "./api.service";
import { ApiType, Order } from "../models/index";
import { distinctUntilChanged, map } from "rxjs/operators";
import { HttpParams } from "@angular/common/http";
import { OrderDetail } from "../models/orderDetail.model";

@Injectable()
export class OrderService {
  public serviceURL = "/order";

  constructor(private apiService: ApiService) {}

  add(order: Order): Observable<Order> {
    return this.apiService.post(
      this.serviceURL,
      order,
      null,
      ApiType.CryptoManagerServerApi
    );
  }

  getAllByLoggedUser(
    isViaRoboTrader: boolean,
    setupTraderId: string = "",
    startDate: string = "",
    endDate: string = ""
  ): Observable<any> {
    const params = `isViaRoboTrader=${isViaRoboTrader}&setupTraderId=${setupTraderId}&startDate=${startDate}&endDate=${endDate}`;
    return this.apiService.get(
      `${this.serviceURL}/GetOrderDetailsByApplicationUser?${params}`,
      null,
      ApiType.CryptoManagerServerApi
    );
  }

  delete(id: string) {
    return this.apiService.delete(
      this.getUrl(id),
      ApiType.CryptoManagerServerApi
    );
  }

  private getUrl(id) {
    return this.serviceURL + "/" + id;
  }
}
