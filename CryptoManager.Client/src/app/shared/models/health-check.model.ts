import { HealthStatusType } from "./health-status-type.enum";

export class HealthCheck {
    name: string;
    status: HealthStatusType;
    description: string;
    errors: any[];
}