import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AssetRoutingModule } from './asset-routing.module';
import { AssetComponent } from './asset.component';
import { AssetFormComponent } from './asset-form/asset-form.component';
import { ErrorsModule, PageHeaderModule } from '../../shared'
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    imports: [CommonModule, AssetRoutingModule, ErrorsModule, PageHeaderModule, TranslateModule, FormsModule],
    declarations: [AssetComponent, AssetFormComponent]
})
export class AssetModule {}
