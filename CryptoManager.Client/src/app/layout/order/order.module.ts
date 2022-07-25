import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { OrderRoutingModule } from './order-routing.module';
import { OrderComponent } from './order.component';
import { PageHeaderModule } from '../../shared';
import { TranslateModule } from '@ngx-translate/core';
import { OrderFormComponent } from './order-form/order-form.component';
import { NgSelectModule } from '@ng-select/ng-select';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { TextMaskModule } from 'angular2-text-mask';

@NgModule({
    imports: [CommonModule, 
              OrderRoutingModule, 
              NgSelectModule, 
              PageHeaderModule, 
              TranslateModule, 
              FormsModule, 
              NgbModule,
              TextMaskModule,
              ReactiveFormsModule
            ],
    declarations: [OrderComponent, OrderFormComponent]
})
export class OrderModule {}
