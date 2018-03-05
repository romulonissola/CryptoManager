import { Component, OnInit } from '@angular/core';
import { AccountService } from './shared';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
    constructor (
        private accountService: AccountService
    ) {}

    ngOnInit() {
        this.accountService.populate();
    }
}
