import { ExchangeType } from "./exchangeType.enum";

export class Exchange {
    id: string;
    name: string;
    website: string;
    apiUrl: string;
    isEnabled: boolean;
    registryDate: Date;
    exchangeType: ExchangeType;
}