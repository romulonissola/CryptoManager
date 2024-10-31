import { Component, OnInit, NgZone } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { routerTransition } from "../router.animations";
import { Errors, AccountService, HealthService } from "../shared/";
import { HealthStatusType } from "../shared/models/health-status-type.enum";
import { environment } from "../../environments/environment";
declare var particlesJS: any;
declare var google: any;
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
    this.healthService.getAllForCryptoManager().subscribe(
      (data) => (this.generalStatus = data.generalStatus),
      (error) => {
        this.generalStatus = error.error.generalStatus;
      }
    );

    google.accounts.id.initialize({
      client_id: environment.login.google.appId,
      callback: (response: any) => this.onGoogleLogin(response),
    });
    google.accounts.id.renderButton(
      document.getElementById("buttonDiv"),
      {
        type: "standard",
        shape: "pill",
        theme: "filled_black",
        text: "signin_with",
        size: "large",
        logo_alignment: "right",
      } // customization attributes
    );
    google.accounts.id.prompt(); // also display the One Tap dialog
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
          () => alert(error),
          () => alert("API is Offline")
        );
      }
    );
  }

  onGoogleLogin(response: any) {
    this.errors = { errors: {} };
    this.accountService.googleLogin(response.credential).subscribe(
      () => {
        this.zone.run(() => this.router.navigateByUrl(this.returnUrl));
      },
      (err) => {
        const error = JSON.stringify(err);
        this.healthService.ping().subscribe(
          () => alert(error),
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
