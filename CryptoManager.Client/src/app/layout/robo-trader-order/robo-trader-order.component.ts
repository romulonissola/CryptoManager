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

@Component({
  selector: "app-robo-trader-order",
  templateUrl: "./robo-trader-order.component.html",
  styleUrls: ["./robo-trader-order.component.scss"],
  animations: [routerTransition()],
})
export class RoboTraderOrderComponent implements OnInit {
  orders: OrderDetail[] = [];
  setupTraders: SetupTrader[] = [];
  selectedSetupTraderId: string = null;
  startDate = this.formatDate(new Date());
  endDate = this.formatDate(this.addDaysToDate(new Date(), 1));
  numberOfTrades = 0;
  totalProfits = 0;

  constructor(
    private translate: TranslateService,
    private orderService: OrderService,
    private setupTraderService: SetupTraderService,
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

  search() {
    this.orderService
      .getAllByLoggedUser({
        isViaRoboTrader: true,
        isBackTest: false,
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
