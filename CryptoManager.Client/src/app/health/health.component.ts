import { Component, OnInit } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { HealthService } from "../shared";
import { HealthStatusType } from "../shared/models/health-status-type.enum";
import { Health } from "../shared/models/health.model";

@Component({
  selector: "app-health",
  templateUrl: "./health.component.html",
  styleUrls: ["./health.component.scss"],
})
export class HealthComponent implements OnInit {
  health: Health = {
    generalStatus: HealthStatusType.Healthy,
    checks: [],
  };

  constructor(private healthService: HealthService) {}

  ngOnInit() {
    this.healthService.getAll().subscribe(
      (data) => (this.health = data),
      (error) => {
        this.health = error.error;
      }
    );
  }

  getColorByStatus(status: HealthStatusType) {
    switch (status) {
      case HealthStatusType.Degraded:
        return "table-warning";
      case HealthStatusType.Unhealthy:
        return "table-danger";
      case HealthStatusType.Healthy:
        return "table-success";
    }
  }
}
