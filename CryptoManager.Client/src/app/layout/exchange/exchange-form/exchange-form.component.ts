import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { routerTransition } from '../../../router.animations';
import { TranslateService } from '@ngx-translate/core';
import { Exchange, ExchangeService } from '../../../shared'

@Component({
  selector: 'app-exchange-form  ',
  templateUrl: './exchange-form.component.html',
  styleUrls: ['./exchange-form.component.scss'],
  animations: [routerTransition()]
})
export class ExchangeFormComponent implements OnInit {
  title: string;
  exchange: Exchange = new Exchange();
  exchangesType= [
    { 
      id: 0, name: 'Binance'
    },
    {
      id: 1, name: 'HitBTC'
    },
    {
      id: 2, name: 'Coinbase'
    },
    {
      id: 3, name: 'BitcoinTrade'
    },
    {
      id: 4, name: 'KuCoin'
    }
  ];
  formState: string;
  constructor(private translate:TranslateService,
              private exchangeService: ExchangeService,
              private router: Router,
              private route: ActivatedRoute) { }

  ngOnInit() {
    var id = this.route.params.subscribe(params => {
      var id = params['id'];

      this.formState = id ? "Edit" : "Add";
      this.title = this.translate.instant(this.formState);
      if (!id)
        return;

      this.exchangeService.get(id)
         .subscribe(
           exchange => this.exchange = exchange,
           response => {
             if (response.status == 404) {
               this.router.navigate(['NotFound']);
             }
           });
    });
  }

  save() {
    var result;
    if (this.exchange.id){
      result = this.exchangeService.update(this.exchange);
    } else {
      result = this.exchangeService.add(this.exchange);
    }

    result.subscribe(data => this.router.navigate(['exchange']));
  }

}
