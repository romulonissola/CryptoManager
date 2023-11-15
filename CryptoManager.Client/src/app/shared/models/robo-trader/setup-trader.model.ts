import { ExchangeType } from "../exchangeType.enum";
import { TradingStatusType } from "./trading-status-type.enum";
import { TradingStrategyType } from "./trading-strategy-type.enum";

export class SetupTrader {
  id: string;
  exchange: ExchangeType;
  //assetsToTrade: AssetToTradeDTO[];

  quoteAssetId: string;
  //quoteAsset: AssetDTO;
  amountToTrade: number;
  minimumAmountToTrade: number | null;

  //emaIndicatorsToStart: ExponentialMovingAverageIndicatorParameterDTO[];
  //rsiIndicatorsToStart: RelativeStrengthIndexIndicatorParameterDTO[];
  //smaIndicatorsToStart: SimpleMovingAverageIndicatorParameterDTO[];
  //priceIndicatorsToStart: PriceIndicatorParameterDTO[];

  tradingStrategy: TradingStrategyType;
  tradingStatus: TradingStatusType;

  isRepeatable: boolean;
  isTestMode: boolean;
  startExpression: string;
  stopExpression: string;
  finishReason: string | null;
  strategyName: string | null;
  //emaIndicatorsToStop: ExponentialMovingAverageIndicatorParameterDTO[];
  //rsiIndicatorsToStop: RelativeStrengthIndexIndicatorParameterDTO[];
  //smaIndicatorsToStop: SimpleMovingAverageIndicatorParameterDTO[];
  //priceIndicatorsToStop: PriceIndicatorParameterDTO[];

  isExcluded: boolean;
  isEnabled: boolean;
  registryDate: Date;
}
