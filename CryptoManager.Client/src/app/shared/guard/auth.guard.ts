import { Injectable } from '@angular/core';
import { Router, CanActivate, CanActivateChild , ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable()
export class AuthGuard implements CanActivate, CanActivateChild {
    constructor(private router: Router) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean{
        if (localStorage.getItem('isLoggedin') == "true") {
            return true;
        }

        this.router.navigate(['login'], { queryParams: { returnUrl: state.url }});
        return false;
    }

    canActivateChild(activatedRoute: ActivatedRouteSnapshot, routerState: RouterStateSnapshot): boolean {
        return this.canActivate(activatedRoute, routerState);
    }
}
