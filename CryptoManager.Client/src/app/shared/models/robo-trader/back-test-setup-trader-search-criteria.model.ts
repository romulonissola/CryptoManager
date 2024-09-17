import { BackTestStatusType } from "./back-test-status-type.enum";

export class BackTestSetupTraderSearchCriteria {
  setupTraderId: string;
  status?: BackTestStatusType;
  fromDate?: string;
  toDate?: string;
}
