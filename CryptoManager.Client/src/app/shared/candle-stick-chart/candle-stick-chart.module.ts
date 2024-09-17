import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { TranslateModule } from "@ngx-translate/core";
import { CandleStickChartComponent } from "./candle-stick-chart.component";

@NgModule({
  imports: [CommonModule, NgbModule, TranslateModule],
  declarations: [CandleStickChartComponent],
  exports: [CandleStickChartComponent],
})
export class CandleStickChartModule {}
