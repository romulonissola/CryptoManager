import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BackTestTraderOrderRoutingModule } from "./back-test-trader-order-routing.module";
import { NgSelectModule } from "@ng-select/ng-select";
import { PageHeaderModule, SharedPipesModule } from "../../shared";
import { TranslateModule } from "@ngx-translate/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { BackTestTraderOrderComponent } from "./back-test-trader-order.component";
import { CandleStickChartModule } from "../../shared/candle-stick-chart/candle-stick-chart.module";
@NgModule({
  declarations: [BackTestTraderOrderComponent],
  imports: [
    CommonModule,
    BackTestTraderOrderRoutingModule,
    NgSelectModule,
    PageHeaderModule,
    TranslateModule,
    FormsModule,
    NgbModule,
    ReactiveFormsModule,
    SharedPipesModule,
    CandleStickChartModule,
  ],
})
export class BackTestTraderOrderModule {}
