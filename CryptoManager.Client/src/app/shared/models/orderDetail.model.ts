export class OrderDetail {
  id: string;
  boughtDate: Date;
  soldDate: Date | null;
  exchangeName: string;
  baseAssetSymbol: string;
  quoteAssetSymbol: string;
  avgPrice: number;
  quantity: number;
  valuePaidWithFees: number;
  currentPrice: number;
  valueSoldWithFees: number;
  profit: number;
  isCompleted: boolean;
}
