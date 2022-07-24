import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AlertComponent } from './alert.component';

@NgModule({
    imports: [CommonModule, NgbModule],
    declarations: [AlertComponent],
    exports: [AlertComponent]
})
export class AlertModule {}