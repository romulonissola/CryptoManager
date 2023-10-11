import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

import { ApiService } from "./api.service";
import { Order, SetupTrader } from "../models/index";
import { ApiType } from "../models/api-type.enum";

@Injectable()
export class SetupTraderService {
  public serviceURL = "/setuptrader";

  constructor(private apiService: ApiService) {}

  getAllByLoggedUser(): Observable<SetupTrader[]> {
    return this.apiService.get(
      `${this.serviceURL}/GetByApplicationUser`,
      null,
      ApiType.RoboTraderApi
    );
  }
}
