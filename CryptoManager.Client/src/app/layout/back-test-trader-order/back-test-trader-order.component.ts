import { Component, OnInit } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
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
  orders: OrderDetail[] = [];
  setupTraders: SetupTrader[] = [];
  selectedSetupTraderId: string = null;
  startDate = this.formatDate(new Date());
  endDate = this.formatDate(this.addDaysToDate(new Date(), 1));
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
    this.search();
  }

  searchBackTests() {
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

  search() {
    this.orderService
      .getAllByLoggedUser({
        isViaRoboTrader: true,
        isBackTest: true,
        setupTraderId: this.selectedSetupTraderId
          ? this.selectedSetupTraderId
          : "",
        startDate: this.startDate ? this.startDate.toString() : "",
        endDate: this.endDate ? this.endDate.toString() : "",
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

  getProfitColor(profit: number) {
    if (profit >= 0) {
      return "green";
    }
    return "red";
  }

  private formatDate(date: Date) {
    const d = new Date(date);
    let month = "" + (d.getMonth() + 1);
    let day = "" + d.getDate();
    const year = d.getFullYear();
    if (month.length < 2) month = "0" + month;
    if (day.length < 2) day = "0" + day;
    return [year, month, day].join("-");
  }

  addDaysToDate(date: Date, days: number): Date {
    var date = new Date(date);
    date.setDate(date.getDate() + days);
    return date;
  }
}
