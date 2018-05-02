import { OrderItem } from "./orderItem.model";

export class Order {
    id: string;
    date: Date;
    exchangeId:string;
    exchangeName: string;
    baseAssetId: string;
    baseAssetSymbol: string;
    quoteAssetId: string;
    quoteAssetSymbol: string;
    avgPrice: number;
    quantity: number;
    valuePaidWithFees: number;
    currentPrice: number;
    valueSoldWithFees: number;
    profit: number;
    items: OrderItem[];
}