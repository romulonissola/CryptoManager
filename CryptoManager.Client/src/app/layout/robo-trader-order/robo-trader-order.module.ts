import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoboTraderOrderRoutingModule } from './robo-trader-order-routing.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { PageHeaderModule } from '../../shared';
import { TranslateModule } from '@ngx-translate/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TextMaskModule } from 'angular2-text-mask';
import { RoboTraderOrderComponent } from './robo-trader-order.component';

@NgModule({
  declarations: [RoboTraderOrderComponent],
  imports: [CommonModule,
    RoboTraderOrderRoutingModule,
    NgSelectModule,
    PageHeaderModule,
    TranslateModule,
    FormsModule,
    NgbModule,
    TextMaskModule,
    ReactiveFormsModule
  ],
})

export class RoboTraderOrderModule { }