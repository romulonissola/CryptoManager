import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { ExchangeRoutingModule } from './exchange-routing.module';
import { ExchangeComponent } from './exchange.component';
import { ExchangeFormComponent } from './exchange-form/exchange-form.component';
import { ErrorsModule } from '../../shared'
import { PageHeaderModule } from '../../shared';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    imports: [CommonModule, ExchangeRoutingModule, ErrorsModule, PageHeaderModule, TranslateModule, FormsModule],
    declarations: [ExchangeComponent, ExchangeFormComponent]
})
export class ExchangeModule {}
