import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { ReplaySubject } from 'rxjs/ReplaySubject';

import { ApiService } from './api.service';
import { JwtService } from './jwt.service';
import { User } from '../models/user.model';
import { distinctUntilChanged, map } from 'rxjs/operators';
import { JwtHelper } from '../helpers/index';
import { HttpParams } from '@angular/common/http';
declare const FB: any;

@Injectable()
export class AccountService {
  public userFields: string[] = [
    'email',
    'id',
    'name',
    'picture'
  ];
  public fbApiPermission: string[] = [
      'email'
  ];

  public serviceURL = "/account";
  private currentUserSubject = new BehaviorSubject<User>({} as User);
  public currentUser = this.currentUserSubject.asObservable().pipe(distinctUntilChanged());

  private isAuthenticatedSubject = new ReplaySubject<boolean>(1);
  public isAuthenticated = this.isAuthenticatedSubject.asObservable();

  constructor (
    private apiService: ApiService,
    private jwtService: JwtService,
    private jwtHelper: JwtHelper,
  ) {}

  populate() {
    if (this.jwtService.getToken()) {
      if(this.currentUserSubject.value.toString() == "[object Object]"){
        let tokenDecoded = this.jwtHelper.decodeToken(this.jwtService.getToken());
        let user: User = new User();
        user.id = tokenDecoded.Id;
        user.email = tokenDecoded.Email;
        user.imageURL = tokenDecoded.PictureURL;
        user.username = tokenDecoded.Name;
        user.token = this.jwtService.getToken();
        this.setAuth(user);
      }
    } else {
      this.purgeAuth();
    }
  }

  setAuth(user: User) {
    this.jwtService.saveToken(user.token);
    this.currentUserSubject.next(user);
    this.isAuthenticatedSubject.next(true);
    localStorage.setItem('isLoggedin', 'true');
  }

  purgeAuth() {
    this.jwtService.destroyToken();
    this.currentUserSubject.next({} as User);
    this.isAuthenticatedSubject.next(false);
    localStorage.setItem('isLoggedin', 'false');
  }

  facebookLogin(): Observable<boolean> {
    return new Observable<boolean>( observer => {
      FB.getLoginStatus((response) => {
        if (response.status === 'connected') {
          return this.authFacebookUser(response.authResponse.accessToken)
                                      .subscribe(
                                        data =>
                                        { 
                                          observer.next(data);
                                        },
                                        error=> {
                                          observer.error(error);
                                        }
                                      );
        } else {
          FB.login((loginResponse)=>{
            if(loginResponse.status !== 'not_authorized'){
              return this.authFacebookUser(loginResponse.authResponse.accessToken)
                                      .subscribe(
                                        data =>
                                        { 
                                          observer.next(data);
                                        },
                                        error=> {
                                          observer.error(error);
                                        }
                                      );
            } else {
              this.purgeAuth();
              observer.error("User Not logged");
            }
          }, {scope: this.fbApiPermission.join(',')});
        }
      });
    }); 
  }

  authFacebookUser(accessToken: string): Observable<boolean> {
    let httpParams = new HttpParams()
    .append("accessToken", accessToken);
    return this.apiService.post(this.serviceURL + '/ExternalLoginFacebook', null, httpParams)
        .pipe(map(data => {
          let tokenDecoded = this.jwtHelper.decodeToken(data);
          let user: User = new User();
          user.id = tokenDecoded.Id;
          user.email = tokenDecoded.Email;
          user.imageURL = tokenDecoded.PictureURL;
          user.username = tokenDecoded.Name;
          user.token = data;
          this.setAuth(user);
          return true;
        }));
  }

  getCurrentUser(): User {
    return this.currentUserSubject.value;
  }
}