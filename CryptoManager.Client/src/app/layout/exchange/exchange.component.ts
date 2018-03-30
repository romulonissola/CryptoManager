import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../router.animations';
import { TranslateService } from '@ngx-translate/core';
import { Exchange, ExchangeService } from '../../shared'
@Component({
  selector: 'app-exchange',
  templateUrl: './exchange.component.html',
  styleUrls: ['./exchange.component.scss'],
  animations: [routerTransition()]
})
export class ExchangeComponent implements OnInit {
  exchanges: Exchange[] = [];

  constructor(private translate: TranslateService, private exchangeService: ExchangeService) {}

   ngOnInit() {
    this.exchangeService.getAll()
      .subscribe(data => this.exchanges = data);
  }

  delete(exchange){
    if (confirm(this.translate.instant("DeleteMessage"))) {
      var index = this.exchanges.indexOf(exchange);
      this.exchanges.splice(index, 1);
      this.exchangeService.delete(exchange.id)
        .subscribe(null,
          err => {
            alert("Could not delete.");
            // Revert the view back to its original state
            this.exchanges.splice(index, 0, exchange);
          });
    }
  }

}
