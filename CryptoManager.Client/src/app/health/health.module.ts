import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ErrorsModule } from '../shared'
import { PageHeaderModule } from '../shared';
import { TranslateModule } from '@ngx-translate/core';
import { HealthComponent } from './health.component';
import { HealthRoutingModule } from './health-routing.module';

@NgModule({
    imports: [CommonModule, HealthRoutingModule, ErrorsModule, PageHeaderModule, TranslateModule, FormsModule],
    declarations: [HealthComponent]
})
export class HealthModule {}
