import { Component, OnInit } from '@angular/core';
import { routerTransition } from '../../router.animations';
import { TranslateService } from '@ngx-translate/core';
import { Exchange, ExchangeService, ExchangeType, AccountService, User, AlertHandlerService, AlertType } from '../../shared'
import { ConfirmationModalComponent } from '../../shared/confirmation-modal';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-exchange',
  templateUrl: './exchange.component.html',
  styleUrls: ['./exchange.component.scss'],
  animations: [routerTransition()]
})
export class ExchangeComponent implements OnInit {
  exchanges: Exchange[] = [];
  user: User;

  constructor(
    private translate: TranslateService,
    private exchangeService: ExchangeService,
    private accountService: AccountService,
    private alertHandlerService: AlertHandlerService,
    private modalService: NgbModal) {}

  ngOnInit() {
    this.user = this.accountService.getCurrentUser();
    this.exchangeService.getAll()
      .subscribe(
        data => this.exchanges = data,
        () => this.alertHandlerService.createAlert(AlertType.Danger, this.translate.instant('CouldNotProcess')));
  }

  delete(exchange){
    const modalRef = this.modalService.open(ConfirmationModalComponent);
    modalRef.componentInstance.confirmationBoxTitle = this.translate.instant('Exchanges');
    modalRef.componentInstance.confirmationMessage = this.translate.instant('DeleteMessage');
    
    modalRef.result.then((userResponse) => {
      if (userResponse) {
        var index = this.exchanges.indexOf(exchange);
        this.exchanges.splice(index, 1);
        this.exchangeService.delete(exchange.id)
          .subscribe(
            () => this.alertHandlerService.createAlert(AlertType.Success, "Exchange Deleted"),
            () => {
              this.alertHandlerService.createAlert(AlertType.Danger, this.translate.instant('CouldNotProcess'));
              // Revert the view back to its original state
              this.exchanges.splice(index, 0, exchange);
            });
      }
    }).catch((error)=> (console.log(`User aborted: ${error}`)));
  }
  getExchangeType(exchangeType){
    return ExchangeType[exchangeType];
  }

}
