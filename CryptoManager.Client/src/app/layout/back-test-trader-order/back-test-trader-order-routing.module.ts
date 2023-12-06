import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { BackTestTraderOrderComponent } from "./back-test-trader-order.component";

const routes: Routes = [
  { path: "", component: BackTestTraderOrderComponent, pathMatch: "full" },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BackTestTraderOrderRoutingModule {}
