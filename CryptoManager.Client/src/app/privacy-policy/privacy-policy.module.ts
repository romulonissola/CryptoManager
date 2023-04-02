import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrivacyPolicyComponent } from './privacy-policy.component';
import { PrivatePolicyRoutingModule } from './privacy-policy-routing.module';

@NgModule({
  declarations: [
    PrivacyPolicyComponent
  ],
  imports: [
    CommonModule,
    PrivatePolicyRoutingModule
  ]
})
export class PrivacyPolicyModule { }
