import { Component, OnInit } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import {
  AlertHandlerService,
  AlertType,
  OrderService,
  SetupTrader,
  SetupTraderService,
} from "../../shared";
import { OrderDetail } from "../../shared/models/orderDetail.model";
import { take } from "rxjs/operators";
import { routerTransition } from "../../router.animations";
import { BackTestSetupTraderService } from "../../shared/services/back-test-setup-trader.service";
import { BackTestStatusType } from "../../shared/models/back-test-status-type.enum";
import { BackTestSetupTrader } from "../../shared/models/back-test-setup-trader.model";

@Component({
  selector: "app-back-test-trader-order",
  templateUrl: "./back-test-trader-order.component.html",
  styleUrls: ["./back-test-trader-order.component.scss"],
  animations: [routerTransition()],
})
export class BackTestTraderOrderComponent implements OnInit {
  backTestStatusType = BackTestStatusType;
  orders: OrderDetail[] = [];
  setupTraders: SetupTrader[] = [];
  selectedSetupTraderId: string = null;
  numberOfTrades = 0;
  totalProfits = 0;
  backTestStatus: BackTestStatusType = null;
  backTests: BackTestSetupTrader[] = [];

  constructor(
    private translate: TranslateService,
    private orderService: OrderService,
    private setupTraderService: SetupTraderService,
    private backTestSetupTraderService: BackTestSetupTraderService,
    private alertHandlerService: AlertHandlerService
  ) {}

  ngOnInit() {
    this.setupTraderService
      .getAllByLoggedUser()
      .pipe(take(1))
      .subscribe(
        (data) => (this.setupTraders = data),
        () =>
          this.alertHandlerService.createAlert(
            AlertType.Danger,
            this.translate.instant("CouldNotProcess")
          )
      );
  }

  searchBackTests() {
    this.backTests = [];
    if (this.selectedSetupTraderId) {
      this.backTestSetupTraderService
        .getByCriteria({
          setupTraderId: this.selectedSetupTraderId,
          status: this.backTestStatus,
        })
        .pipe(take(1))
        .subscribe(
          (data) => (this.backTests = data),
          () =>
            this.alertHandlerService.createAlert(
              AlertType.Danger,
              this.translate.instant("CouldNotProcess")
            )
        );
    }
  }

  search(startDate: string, endDate: string) {
    this.orderService
      .getAllByLoggedUser({
        isViaRoboTrader: true,
        isBackTest: true,
        setupTraderId: this.selectedSetupTraderId
          ? this.selectedSetupTraderId
          : "",
        startDate,
        endDate,
      })
      .pipe(take(1))
      .subscribe(
        (data) => {
          this.orders = data;

          this.numberOfTrades = this.orders.length;
          this.totalProfits = this.orders.reduce(
            (total, s) => total + s.profit,
            0
          );
        },
        () =>
          this.alertHandlerService.createAlert(
            AlertType.Danger,
            this.translate.instant("CouldNotProcess")
          )
      );
  }

  continueFaulty(backTestSetupTraderId: string) {
    this.backTestSetupTraderService
      .continueFaulty(backTestSetupTraderId)
      .pipe(take(1))
      .subscribe(
        (data) => {
          if (!data.hasSucceded) {
            this.alertHandlerService.createAlert(
              AlertType.Danger,
              data.errorMessage
            );
          }
          this.alertHandlerService.createAlert(
            AlertType.Success,
            this.translate.instant("Succeded")
          );
        },
        () =>
          this.alertHandlerService.createAlert(
            AlertType.Danger,
            this.translate.instant("CouldNotProcess")
          )
      );
  }

  getProfitColor(profit: number) {
    if (profit >= 0) {
      return "green";
    }
    return "red";
  }

  getBackTestStatusDescription(status: BackTestStatusType) {
    switch (status) {
      case BackTestStatusType.Running:
        return "Running";
      case BackTestStatusType.Faulty:
        return "Faulty";
      case BackTestStatusType.Finished:
        return "Finished";
    }
  }
}
