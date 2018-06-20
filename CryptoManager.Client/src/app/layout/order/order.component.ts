import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../router.animations';
import { TranslateService } from '@ngx-translate/core';
import { OrderService } from '../../shared';
import { OrderDetail } from '../../shared/models/orderDetail.model';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
  animations: [routerTransition()]
})
export class OrderComponent implements OnInit {
  orders: OrderDetail[] = [];
  constructor(private translate: TranslateService, private orderService: OrderService) { }

  ngOnInit() {
    this.orderService.getAllByLoggedUser()
      .subscribe(data => this.orders = data);
  }

  getProfitColor(profit:number){
    if(profit >= 0){
      return "green";
    }
    return "red";
  }

  delete(order){
    if (confirm(this.translate.instant("DeleteMessage"))) {
      var index = this.orders.indexOf(order);
      this.orders.splice(index, 1);
      this.orderService.delete(order.id)
        .subscribe(null,
          err => {
            alert("Could not delete.");
            // Revert the view back to its original state
            this.orders.splice(index, 0, order);
          });
    }
  }
}
