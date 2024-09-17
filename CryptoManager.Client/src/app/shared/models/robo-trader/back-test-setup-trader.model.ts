import { BackTestStatusType } from "./back-test-status-type.enum";

export class BackTestSetupTrader {
  id: string;
  setupTraderId: string;
  status: BackTestStatusType;
  fromDate: string;
  toDate: string;
  currentDate: string;
  errorMessage: string;
}
