import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OrderComponent } from './order.component';
import { OrderFormComponent } from './order-form/order-form.component';

const routes: Routes = [
    { path: '', component: OrderComponent, pathMatch: 'full' },
    { path: 'new', component: OrderFormComponent }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class OrderRoutingModule {}