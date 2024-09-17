import { IntervalType } from "./interval-type.enum";

export class GetCandlesForChartParams {
  fromDate: string;
  toDate: string;
  intervalType: IntervalType;
  baseAssetSymbol: string;
  quoteAssetSymbol: string;
}
