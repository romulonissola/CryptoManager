import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AssetComponent } from './asset.component';
import { AssetFormComponent } from './asset-form/asset-form.component';

const routes: Routes = [
    { path: '', component: AssetComponent, pathMatch: 'full' },
    { path: 'new', component: AssetFormComponent},
    { path: ':id', component: AssetFormComponent}
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AssetRoutingModule {}
