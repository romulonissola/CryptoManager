import { Component, OnInit, ViewChild } from "@angular/core";
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
import { BackTestStatusType } from "../../shared/models/robo-trader/back-test-status-type.enum";
import { BackTestSetupTrader } from "../../shared/models/robo-trader/back-test-setup-trader.model";
import { ChartService } from "../../shared/services/chart.service";
import { IntervalType } from "../../shared/models/robo-trader/interval-type.enum";
import { CandleStick } from "../../shared/models/robo-trader/candle-stick.model";
import { CandleStickChartComponent } from "../../shared/candle-stick-chart/candle-stick-chart.component";

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
  candles: CandleStick[] = [];

  @ViewChild("chart") chart: CandleStickChartComponent;
  constructor(
    private translate: TranslateService,
    private orderService: OrderService,
    private setupTraderService: SetupTraderService,
    private backTestSetupTraderService: BackTestSetupTraderService,
    private chartService: ChartService,
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

  search(backTest: BackTestSetupTrader) {
    this.getOrders(backTest, backTest.fromDate, backTest.toDate);
  }

  getOrders(backTest: BackTestSetupTrader, startDate: string, endDate: string) {
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
          this.getCandles(backTest);
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

  getCandles(backTest: BackTestSetupTrader) {
    var order = this.orders[0]; //TODO: it can have more pairs, create chart for each
    if (order) {
      this.chartService
        .getCandles({
          baseAssetSymbol: order.baseAssetSymbol,
          quoteAssetSymbol: order.quoteAssetSymbol,
          fromDate: backTest.fromDate,
          toDate: backTest.toDate,
          intervalType: IntervalType.OneHour,
        })
        .pipe(take(1))
        .subscribe(
          (data) => {
            if (!data.hasSucceded) {
              this.alertHandlerService.createAlert(
                AlertType.Danger,
                data.errorMessage
              );
            } else {
              this.candles = data.item;
              this.chart.renderChart(this.candles, this.orders);
            }
          },
          () =>
            this.alertHandlerService.createAlert(
              AlertType.Danger,
              this.translate.instant("CouldNotProcess")
            )
        );
    }
  }
}
