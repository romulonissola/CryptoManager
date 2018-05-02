import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { routerTransition } from '../../../router.animations';
import { TranslateService } from '@ngx-translate/core';
import { Exchange, Order, ExchangeService, AssetService, Asset, OrderItem} from '../../../shared'
import { Observable } from 'rxjs/Observable';
import createNumberMask from 'text-mask-addons/dist/createNumberMask';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

const numberMask = createNumberMask({
  prefix: '',
  thousandsSeparatorSymbol: '.',
  allowDecimal: true,
  decimalSymbol: ',',
  decimalLimit: 8
})

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.scss'],
  animations: [routerTransition()]
})
export class OrderFormComponent implements OnInit {
  order: Order = new Order();
  orderItem: OrderItem = new OrderItem();
  exchanges: Exchange[] = [];
  assets: Asset[] = [];
  orderItems: OrderItem[] = [];
  select: any;
  public mask: Array<string | RegExp> = numberMask;
  orderGroup: FormGroup;
  orderItemGroup: FormGroup;
  
  constructor(private translate: TranslateService,
              private exchangeService: ExchangeService,
              private assetService: AssetService,
              private router: Router,
              private route: ActivatedRoute,
              private frmBuilder: FormBuilder) { }

  ngOnInit() {
    this.exchangeService.getAll().subscribe(items => this.exchanges = items);
    this.assetService.getAll().subscribe(items => this.assets = items);

    this.orderGroup = this.frmBuilder.group({
      date:["", [Validators.required]],
      exchange:["", [Validators.required]],
      baseAsset:["", [Validators.required]],
      quoteAsset:["", [Validators.required]]
    });

    this.orderItemGroup = this.frmBuilder.group({
      price:["", [Validators.required]],
      quantity:["", [Validators.required]],
      fee:["", [Validators.required]],
      feeAsset:["", [Validators.required]]
    });
  }

  get date() { return this.orderGroup.get('date'); }
  get exchange() { return this.orderGroup.get('exchange'); }
  get baseAsset() { return this.orderGroup.get('baseAsset'); }
  get quoteAsset() { return this.orderGroup.get('quoteAsset'); }

  get price() { return this.orderItemGroup.get('price'); }
  get quantity() { return this.orderItemGroup.get('quantity'); }
  get fee() { return this.orderItemGroup.get('fee'); }
  get feeAsset() { return this.orderItemGroup.get('feeAsset'); }

  addItem() {
    this.orderItems.splice(this.orderItems.length, 0, Object.assign({}, this.orderItem));
    this.orderItem = new OrderItem();
    this.select = null;
  }

  deleteItem(orderItem){
    if (confirm(this.translate.instant("DeleteMessage"))) {
      var index = this.orderItems.indexOf(orderItem);
      this.orderItems.splice(index, 1);
    }
  }

  changeFeeAsset(select: Asset){
    this.orderItem.feeAssetId = select.id;
    this.orderItem.feeAssetName = select.name;
  }

  save(){

  }
}
