import { OrderItem } from "./orderItem.model";

export class Order {
    id: string;
    date: string;
    exchangeId:string;
    baseAssetId: string;
    quoteAssetId: string;
    orderItems: OrderItem[];
}