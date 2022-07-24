import { Component, OnDestroy, OnInit } from '@angular/core';
import { interval, Subscription } from 'rxjs';
import { take } from 'rxjs/operators';
import { AlertType } from '..';
import { AlertHandlerService } from '../services';

@Component({
    selector: 'app-alert',
    templateUrl: './alert.component.html',
    styleUrls: ['./alert.component.scss']
})
export class AlertComponent implements OnInit, OnDestroy {
    alerts: Array<any> = [];
    subscription: Subscription;
    constructor(private alertHandlerService: AlertHandlerService) {
        this.subscription = alertHandlerService.alert$.subscribe(
            alert => {
                const createdAlert = this.createAlert(alert.alertType, alert.message);
                interval(3000).pipe(take(1)).subscribe(x => {
                    this.closeAlert(createdAlert);
                });
            });
    }

    ngOnInit() {}

    ngOnDestroy() {
        // prevent memory leak when component destroyed
        this.subscription.unsubscribe();
      }

    public closeAlert(alert: any) {
        const index: number = this.alerts.indexOf(alert);
        this.alerts.splice(index, 1);
    }

    public createAlert(alertType: AlertType, message: string) : any {
        const alert = {
            id: this.getId(),
            type: alertType.toString(),
            message
        };

        this.alerts.push(alert);
        return alert;
    }

    getId(){
        if(this.alerts.length > 0){
            return this.alerts[this.alerts.length - 1].id + 1;
        }
        return 1;
    }
}
