import { HealthCheck } from "./health-check.model";
import { HealthStatusType } from "./health-status-type.enum";

export class Health {
  generalStatus: HealthStatusType;
  checks?: HealthCheck[];
}
