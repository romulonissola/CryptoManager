import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { TranslateService } from '@ngx-translate/core';
import { AlertHandlerService, AlertType, OrderService } from '../../shared';
import { OrderDetail } from '../../shared/models/orderDetail.model';

@Component({
  selector: 'app-robo-trader-order',
  templateUrl: './robo-trader-order.component.html',
  styleUrls: ['./robo-trader-order.component.scss']
})
export class RoboTraderOrderComponent implements OnInit {
  orders: OrderDetail[] = [];
  constructor(
    private translate: TranslateService,
    private orderService: OrderService,
    private alertHandlerService: AlertHandlerService,
    private modalService: NgbModal) { }

  ngOnInit() {
    this.orderService.getAllByLoggedUser(true)
      .subscribe(
        data => this.orders = data,
        () => this.alertHandlerService.createAlert(AlertType.Danger, this.translate.instant('CouldNotProcess')));
  }

  getProfitColor(profit: number) {
    if (profit >= 0) {
      return "green";
    }
    return "red";
  }

}
