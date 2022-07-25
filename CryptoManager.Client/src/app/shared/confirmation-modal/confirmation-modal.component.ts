import { Component, Input, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
    templateUrl: './confirmation-modal.component.html',
    styleUrls: ['./confirmation-modal.component.scss']
})
export class ConfirmationModalComponent implements OnInit {
    @Input() confirmationBoxTitle: string;
    @Input() confirmationMessage: string;

    constructor(public activeModal: NgbActiveModal) {}

    ngOnInit() {}
}
