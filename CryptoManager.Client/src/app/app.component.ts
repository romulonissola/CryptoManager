import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AccountService, HealthService, HTTPStatus } from './shared';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {
    showLoader:boolean;
    constructor (private accountService: AccountService,
                private healthService: HealthService,
                private httpStatus: HTTPStatus,
                private translate: TranslateService,
                private router: Router) {
        this.translate.addLangs(['en', 'fr', 'ur', 'es', 'it', 'fa', 'de', 'zh-CHS','pt']);
        this.translate.setDefaultLang('pt');
        const browserLang = this.translate.getBrowserLang();
        this.translate.use(browserLang.match(/en|fr|ur|es|it|fa|de|zh-CHS|pt/) ? browserLang : 'pt');
        this.httpStatus
            .getHttpStatus()
                .subscribe((status: boolean) => {
                        this.showLoader = status;
                });
        this.router.events.subscribe(event =>{
            this.ping();
        });
    }

    ping() {
        this.healthService.ping()
            .subscribe(_ => {
                this.accountService.populate();
            },
            error => {
                alert("API is Offline")
                this.accountService.purgeAuth();
            }
        );
    }
}
