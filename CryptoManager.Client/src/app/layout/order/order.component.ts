import { Component, OnInit } from '@angular/core';
import { Order } from '../../shared/models';
import { routerTransition } from '../../router.animations';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss'],
  animations: [routerTransition()]
})
export class OrderComponent implements OnInit {
  orders: Order[] = [];
  constructor() { }

  ngOnInit() {
  }

}
