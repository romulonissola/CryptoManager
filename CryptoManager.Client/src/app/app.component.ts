import { Component, OnInit } from '@angular/core';
import { AccountService, HTTPStatus } from './shared';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
    showLoader:boolean;
    constructor (private accountService: AccountService,
                private httpStatus: HTTPStatus) {
        this.httpStatus.getHttpStatus()
                .subscribe((status: boolean) => {
                        this.showLoader = status;
                });
    }

    ngOnInit() {
        this.accountService.populate();
    }
}
