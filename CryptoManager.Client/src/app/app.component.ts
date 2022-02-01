import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { AccountService, HTTPStatus } from './shared';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
    showLoader:boolean;
    constructor (private accountService: AccountService,
                private httpStatus: HTTPStatus,
                private translate: TranslateService) {
        this.translate.addLangs(['en', 'fr', 'ur', 'es', 'it', 'fa', 'de', 'zh-CHS','pt']);
        this.translate.setDefaultLang('pt');
        const browserLang = this.translate.getBrowserLang();
        this.translate.use(browserLang.match(/en|fr|ur|es|it|fa|de|zh-CHS|pt/) ? browserLang : 'pt');
        this.httpStatus
            .getHttpStatus()
                .subscribe((status: boolean) => {
                        this.showLoader = status;
                });
    }

    ngOnInit() {
        this.accountService.populate();
    }
}
