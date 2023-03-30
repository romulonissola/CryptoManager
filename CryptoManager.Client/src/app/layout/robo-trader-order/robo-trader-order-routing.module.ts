import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RoboTraderOrderComponent } from './robo-trader-order.component';

const routes: Routes = [
    { path: '', component: RoboTraderOrderComponent, pathMatch: 'full' }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class RoboTraderOrderRoutingModule { }