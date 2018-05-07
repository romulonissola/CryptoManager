export class OrderDetail {
    id: string;
    date: Date;
    exchangeName: string;
    baseAssetSymbol: string;
    quoteAssetSymbol: string;
    avgPrice: number;
    quantity: number;
    valuePaidWithFees: number;
    currentPrice: number;
    valueSoldWithFees: number;
    profit: number;
}