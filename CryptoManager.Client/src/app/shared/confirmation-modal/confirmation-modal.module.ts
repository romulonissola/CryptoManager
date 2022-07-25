import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmationModalComponent } from './confirmation-modal.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    imports: [
        CommonModule,
        NgbModule,
        TranslateModule],
    declarations: [ConfirmationModalComponent],
    exports: [ConfirmationModalComponent]
})
export class ConfirmationModalModule {}