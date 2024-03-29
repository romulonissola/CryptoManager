import { Component, OnInit, NgZone } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { routerTransition } from "../router.animations";
import { Errors, AccountService, HealthService } from "../shared/";
import { HealthStatusType } from "../shared/models/health-status-type.enum";
declare var particlesJS: any;
@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.scss"],
  animations: [routerTransition()],
})
export class LoginComponent implements OnInit {
  errors: Errors = { errors: {} };
  returnUrl: string;
  generalStatus: HealthStatusType;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private zone: NgZone,
    private healthService: HealthService
  ) {}

  ngOnInit() {
    if (localStorage.getItem("isLoggedin") == "true") {
      this.router.navigate(["/"]);
    }
    this.returnUrl = this.route.snapshot.queryParams["returnUrl"] || "/";
    particlesJS.load("particles-js", "assets/particle.json");
    this.healthService.getAll().subscribe(
      (data) => (this.generalStatus = data.generalStatus),
      (error) => {
        this.generalStatus = error.error.generalStatus;
      }
    );
  }

  onFacebookLogin() {
    this.errors = { errors: {} };
    this.accountService.facebookLogin().subscribe(
      () => {
        this.zone.run(() => this.router.navigateByUrl(this.returnUrl));
      },
      (err) => {
        const error = JSON.stringify(err);
        this.healthService.ping().subscribe(
          () => alert(JSON.stringify(error)),
          () => alert("API is Offline")
        );
      }
    );
  }

  getColorByStatus(status) {
    switch (status) {
      case "Degraded":
        return "health-degraded";
      case "Unhealthy":
        return "health-unhealthy";
      case "Healthy":
        return "health-healthy";
      default:
        return "health-unhealthy";
    }
  }

  onLoggedin() {}
}
