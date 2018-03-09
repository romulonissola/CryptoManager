import { Injectable, Injector } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/observable/throw'
import 'rxjs/add/operator/catch';


@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
    constructor() { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req)
            .catch((error, caught) => {
                //intercept the respons error and displace it to the console
                console.log("Error Occurred");
                console.log(error);
                //return the error to the method that called it
                return Observable.throw(error);
            }) as any;
    }
}