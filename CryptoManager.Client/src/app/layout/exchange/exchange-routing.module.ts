import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ExchangeComponent } from './exchange.component';
import { ExchangeFormComponent } from './exchange-form/exchange-form.component';

const routes: Routes = [
    { path: '', component: ExchangeComponent, pathMatch: 'full' },
    { path: 'new', component: ExchangeFormComponent},
    { path: ':id', component: ExchangeFormComponent}
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class ExchangeRoutingModule {}
