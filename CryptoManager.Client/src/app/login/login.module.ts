import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginRoutingModule } from './login-routing.module';
import { LoginComponent } from './login.component';
import { ErrorsModule } from '../shared'

@NgModule({
    imports: [CommonModule, LoginRoutingModule, ErrorsModule],
    declarations: [LoginComponent]
})
export class LoginModule {}
