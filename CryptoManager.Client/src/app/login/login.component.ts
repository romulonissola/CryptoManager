import { Component, OnInit, NgZone } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { routerTransition } from '../router.animations';
import { Errors, AccountService } from '../shared/';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    animations: [routerTransition()]
})
export class LoginComponent implements OnInit {
    errors = {};
    returnUrl: string;

    constructor(private route: ActivatedRoute,
                private router: Router,
                private accountService: AccountService,
                private zone: NgZone) {}
    
    ngOnInit() {
        if(localStorage.getItem('isLoggedin') == "true"){
            this.router.navigate(['/']);
        }
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    }

    onFacebookLogin(){
        this.errors = {};
        this.accountService
        .facebookLogin()
        .subscribe(
          data => {
            this.zone.run(() => this.router.navigateByUrl(this.returnUrl));
          },
          err => {
            alert(JSON.stringify(err));
            this.errors = err;
          }
        );
    }

    onLoggedin() {
    }
}
