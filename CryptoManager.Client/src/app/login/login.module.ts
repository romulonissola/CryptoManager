import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { ErrorsModule } from '../shared'
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    imports: [CommonModule, LoginRoutingModule, TranslateModule, ErrorsModule],
    declarations: [LoginComponent]
})
export class LoginModule {}
