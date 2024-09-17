import {
  BitmapCoordinatesRenderingScope,
  CanvasRenderingTarget2D,
} from "fancy-canvas";
import {
  AutoscaleInfo,
  Coordinate,
  IChartApi,
  ISeriesApi,
  ISeriesPrimitive,
  ISeriesPrimitivePaneRenderer,
  ISeriesPrimitivePaneView,
  Logical,
  SeriesOptionsMap,
  SeriesType,
  Time,
  isBusinessDay,
  isUTCTimestamp,
} from "lightweight-charts";

class TradePlotChartPluginPaneRenderer implements ISeriesPrimitivePaneRenderer {
  _tradeOrderChartPoint1: TradeOrderChartPoint;
  _tradeOrderChartPoint2: TradeOrderChartPoint;
  _text1: string;
  _text2: string;
  _options: TradePlotChartPluginOptions;

  constructor(
    tradeOrderChartPoint1: TradeOrderChartPoint,
    tradeOrderChartPoint2: TradeOrderChartPoint,
    text1: string,
    text2: string,
    options: TradePlotChartPluginOptions
  ) {
    this._tradeOrderChartPoint1 = tradeOrderChartPoint1;
    this._tradeOrderChartPoint2 = tradeOrderChartPoint2;
    this._text1 = text1;
    this._text2 = text2;
    this._options = options;
  }

  draw(target: CanvasRenderingTarget2D) {
    target.useBitmapCoordinateSpace((scope) => {
      if (
        this._tradeOrderChartPoint1.timeCoordinate === null ||
        this._tradeOrderChartPoint1.priceCoordinate === null ||
        this._tradeOrderChartPoint2.timeCoordinate === null ||
        this._tradeOrderChartPoint2.priceCoordinate === null
      ) {
        return;
      }

      const ctx = scope.context;
      const x1Scaled = Math.round(
        this._tradeOrderChartPoint1.timeCoordinate * scope.horizontalPixelRatio
      );
      const boughtPriceCoordinate = Math.round(
        this._tradeOrderChartPoint1.priceCoordinate * scope.verticalPixelRatio
      );
      const x2Scaled = Math.round(
        this._tradeOrderChartPoint2.timeCoordinate * scope.horizontalPixelRatio
      );
      const soldPriceCoordinate = Math.round(
        this._tradeOrderChartPoint2.priceCoordinate * scope.verticalPixelRatio
      );
      const options = this._options;

      ctx.beginPath();
      let columnSize = x2Scaled - x1Scaled;
      columnSize =
        columnSize == 0
          ? columnSize + 1 * scope.horizontalPixelRatio
          : columnSize;

      const priceStart = this._tradeOrderChartPoint1.isProfitable
        ? boughtPriceCoordinate
        : soldPriceCoordinate;

      var priceEnd = this._tradeOrderChartPoint1.isProfitable
        ? soldPriceCoordinate - boughtPriceCoordinate
        : boughtPriceCoordinate - soldPriceCoordinate;

      priceEnd =
        priceEnd == 0 ? priceEnd + 1 * scope.verticalPixelRatio : priceEnd;

      ctx.rect(x1Scaled, priceStart, columnSize, priceEnd); //column, row, colmun size(width), rowsize(height)
      ctx.fillStyle = this._tradeOrderChartPoint1.isProfitable
        ? options.profitableTradeColor
        : options.unprofitableTradeColor;
      ctx.fill();

      this._drawTextLabel(
        scope,
        this._text1,
        x1Scaled,
        boughtPriceCoordinate,
        true
      );
      this._drawTextLabel(
        scope,
        this._text2,
        x2Scaled,
        soldPriceCoordinate,
        false
      );
    });
  }

  _drawTextLabel(
    scope: BitmapCoordinatesRenderingScope,
    text: string,
    x: number,
    y: number,
    left: boolean
  ) {
    scope.context.font = "12px Arial";
    scope.context.beginPath();
    const offset = 5 * scope.horizontalPixelRatio;
    const textWidth = scope.context.measureText(text);
    const leftAdjustment = left ? textWidth.width + offset * 4 : 0;
    scope.context.fillStyle = this._options.labelBackgroundColor;
    scope.context.roundRect(
      x + offset - leftAdjustment,
      y - 24,
      textWidth.width + offset * 2,
      24 + offset,
      5
    );
    scope.context.fill();
    scope.context.beginPath();
    scope.context.fillStyle = this._options.labelTextColor;
    scope.context.fillText(text, x + offset * 2 - leftAdjustment, y);
  }
}

interface TradeOrderChartPoint {
  timeCoordinate: Coordinate | null;
  priceCoordinate: Coordinate | null;
  tradeOrder: TradeOrder | null;
  isProfitable?: boolean;
}

class TradePlotChartPluginPaneView implements ISeriesPrimitivePaneView {
  _source: TradePlotChartPlugin;
  _tradeOrderChartPoint1: TradeOrderChartPoint = {
    timeCoordinate: null,
    priceCoordinate: null,
    tradeOrder: null,
  };
  _tradeOrderChartPoint2: TradeOrderChartPoint = {
    timeCoordinate: null,
    priceCoordinate: null,
    tradeOrder: null,
  };

  constructor(source: TradePlotChartPlugin) {
    this._source = source;
  }

  update() {
    const series = this._source._series;
    const y1 = series.priceToCoordinate(this._source._tradeOrder1.price);
    const y2 = series.priceToCoordinate(this._source._tradeOrder2.price);
    const timeScale = this._source._chart.timeScale();
    const x1 = timeScale.timeToCoordinate(this._source._tradeOrder1.time);
    const x2 = timeScale.timeToCoordinate(this._source._tradeOrder2.time);
    const isProfitable =
      this._source._tradeOrder1.price < this._source._tradeOrder2.price;
    this._tradeOrderChartPoint1 = {
      timeCoordinate: x1,
      priceCoordinate: y1,
      tradeOrder: this._source._tradeOrder1,
      isProfitable,
    };
    this._tradeOrderChartPoint2 = {
      timeCoordinate: x2,
      priceCoordinate: y2,
      tradeOrder: this._source._tradeOrder2,
      isProfitable,
    };
  }

  renderer() {
    const [dateBought, timeBought] = formattedDateAndTime(
      this._source._tradeOrder1.time
        ? convertTime(this._source._tradeOrder1.time)
        : undefined
    );
    const [dateSold, timeSold] = formattedDateAndTime(
      this._source._tradeOrder2.time
        ? convertTime(this._source._tradeOrder2.time)
        : undefined
    );
    return new TradePlotChartPluginPaneRenderer(
      this._tradeOrderChartPoint1,
      this._tradeOrderChartPoint2,
      `${this._source._tradeOrder1.price.toFixed(
        this._source._options.numberOfDigitsDecimalPrice
      )} - ${dateBought} ${timeBought}`,
      `${this._source._tradeOrder2.price.toFixed(
        this._source._options.numberOfDigitsDecimalPrice
      )} - ${dateSold} ${timeSold}`,
      this._source._options
    );
  }
}

interface TradeOrder {
  time: Time;
  price: number;
}

export interface TradePlotChartPluginOptions {
  labelBackgroundColor: string;
  labelTextColor: string;
  profitableTradeColor: string;
  unprofitableTradeColor: string;
  numberOfDigitsDecimalPrice: number;
}

const defaultOptions: TradePlotChartPluginOptions = {
  labelBackgroundColor: "rgba(0, 0, 0, 0.85)",
  labelTextColor: "rgb(255, 255, 255)",
  profitableTradeColor: "rgba(100, 200, 50, 0.5)",
  unprofitableTradeColor: "rgba(242, 54, 69, 0.5)",
  numberOfDigitsDecimalPrice: 2,
};

export class TradePlotChartPlugin implements ISeriesPrimitive<Time> {
  _chart: IChartApi;
  _series: ISeriesApi<keyof SeriesOptionsMap>;
  _tradeOrder1: TradeOrder;
  _tradeOrder2: TradeOrder;
  _paneViews: TradePlotChartPluginPaneView[];
  _options: TradePlotChartPluginOptions;
  _minPrice: number;
  _maxPrice: number;

  constructor(
    chart: IChartApi,
    series: ISeriesApi<SeriesType>,
    tradeOrder1: TradeOrder,
    tradeOrder2: TradeOrder,
    options?: Partial<TradePlotChartPluginOptions>
  ) {
    this._chart = chart;
    this._series = series;
    this._tradeOrder1 = tradeOrder1;
    this._tradeOrder2 = tradeOrder2;
    this._minPrice = Math.min(this._tradeOrder1.price, this._tradeOrder2.price);
    this._maxPrice = Math.max(this._tradeOrder1.price, this._tradeOrder2.price);
    this._options = {
      ...defaultOptions,
      ...options,
    };
    this._paneViews = [new TradePlotChartPluginPaneView(this)];
  }

  autoscaleInfo(
    startTimePoint: Logical,
    endTimePoint: Logical
  ): AutoscaleInfo | null {
    const p1Index = this._pointIndex(this._tradeOrder1);
    const p2Index = this._pointIndex(this._tradeOrder2);
    if (p1Index === null || p2Index === null) return null;
    if (endTimePoint < p1Index || startTimePoint > p2Index) return null;
    return {
      priceRange: {
        minValue: this._minPrice,
        maxValue: this._maxPrice,
      },
    };
  }

  updateAllViews() {
    this._paneViews.forEach((pw) => pw.update());
  }

  paneViews() {
    return this._paneViews;
  }

  _pointIndex(p: TradeOrder): number | null {
    const coordinate = this._chart.timeScale().timeToCoordinate(p.time);
    if (coordinate === null) return null;
    const index = this._chart.timeScale().coordinateToLogical(coordinate);
    return index;
  }
}

export function convertTime(t: Time): number {
  if (isUTCTimestamp(t)) return t * 1000;
  if (isBusinessDay(t)) return new Date(t.year, t.month, t.day).valueOf();
  const [year, month, day] = t.split("-").map(parseInt);
  return new Date(year, month, day).valueOf();
}

export function displayTime(time: Time): string {
  if (typeof time == "string") return time;
  const date = isBusinessDay(time)
    ? new Date(time.year, time.month, time.day)
    : new Date(time * 1000);
  return date.toLocaleDateString();
}

export function formattedDateAndTime(
  timestamp: number | undefined
): [string, string] {
  if (!timestamp) return ["", ""];
  const dateObj = new Date(timestamp);

  // Format date string
  const year = dateObj.getFullYear();
  const month = dateObj.toLocaleString("default", { month: "short" });
  const date = padLeft(dateObj.getDate(), 2, "0");
  const formattedDate = `${date} ${month} ${year}`;

  // Format time string
  const hours = padLeft(dateObj.getHours(), 2, "0");
  const minutes = padLeft(dateObj.getMinutes(), 2, "0");
  const formattedTime = `${hours}:${minutes}`;

  return [formattedDate, formattedTime];
}

const padLeft = (
  number: number,
  length: number,
  character: string = "0"
): string => {
  let result = String(number);
  for (let i = result.length; i < length; ++i) {
    result = character + result;
  }
  return result;
};
