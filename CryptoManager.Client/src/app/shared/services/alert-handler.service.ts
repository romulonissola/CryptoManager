import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { AlertType } from '../models';

@Injectable()
export class AlertHandlerService {
    private alert = new Subject<any>();
    alert$ = this.alert.asObservable();
    constructor() {}

    // handleErrorResponse(response: any) {
    //     console.log(response);
    //     const responseObject = response ? response.error : null;
    //     const isForbiddenStatus = response && response.status && response.status === 403;
    //     const message = responseObject && responseObject.hasError ?
    //         responseObject.error :
    //         isForbiddenStatus ? 'User has no access to this resource' : 'Unable to complete action';
    //     this.showSnackBar(true, message);
    // }

    // handleCommandResponse(response: CommandResponseEmpty, successMessage: string, customCssClass: string = null): boolean {
    //     if (response && response.hasError) {
    //         this.showSnackBar(true, response.error);
    //         return false;
    //     } else {
    //         if (successMessage) {
    //             this.showSnackBar(false, successMessage, customCssClass);
    //         }
    //         return true;
    //     }
    // }

    createAlert(alertType: AlertType, message: string) {
      this.alert.next({ alertType, message });
    }
}