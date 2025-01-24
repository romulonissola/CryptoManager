import { Component, Input } from "@angular/core";
import { ColorType, UTCTimestamp, createChart } from "lightweight-charts";
import { TradePlotChartPlugin } from "./trade-plot-chart-plugin";
import { CandleStick } from "../models/robo-trader/candle-stick.model";
import { OrderDetail } from "../models/orderDetail.model";

@Component({
  selector: "app-candle-stick-chart",
  templateUrl: "./candle-stick-chart.component.html",
  styleUrl: "./candle-stick-chart.component.css",
})
export class CandleStickChartComponent {
  renderChart(candles: CandleStick[], orders: OrderDetail[]) {
    var candleData = candles.map((a) => {
      return {
        close: a.close,
        high: a.high,
        low: a.low,
        open: a.open,
        time: Math.floor(new Date(a.openTime).getTime() / 1000) as UTCTimestamp,
      };
    });

    const chartOptions = {
      rightPriceScale: {
        visible: true,
      },
      leftPriceScale: {
        visible: true,
      },
      layout: {
        textColor: "black",
        background: { type: ColorType.Solid, color: "#fff" },
      },
      crosshair: {
        mode: 0, // CrosshairMode.Normal
      },
      timeScale: {
        timeVisible: true,
        secondsVisible: true,
      },
      localization: {
        priceFormatter: (p) => p.toFixed(7),
      },
    };
    const elementChart = document.getElementById("chart");
    elementChart.innerHTML = "";

    const chart = createChart(elementChart, chartOptions);
    const candlestickSeries = chart.addCandlestickSeries({
      priceScaleId: "right",
      upColor: "#26a69a",
      downColor: "#ef5350",
      borderVisible: false,
      wickUpColor: "#26a69a",
      wickDownColor: "#ef5350",
    });

    candlestickSeries.setData(candleData);

    const chartDates = candles.map(
      (a) => Math.floor(new Date(a.openTime).getTime() / 1000) as UTCTimestamp
    );
    const customSeriesOrders = orders.map((a) => {
      return {
        time: this.findClosestDate(
          (new Date(a.boughtDate).getTime() / 1000) as UTCTimestamp,
          chartDates
        ),
        boughtDate: this.findClosestDate(
          (new Date(a.boughtDate).getTime() / 1000) as UTCTimestamp,
          chartDates
        ),
        bought: a.avgPrice,
        sold: a.currentPrice,
        soldDate: this.findClosestDate(
          (new Date(a.soldDate).getTime() / 1000) as UTCTimestamp,
          chartDates
        ),
      };
    });

    customSeriesOrders.forEach((customSeriesOrder) => {
      const tradeOrder1 = {
        time: customSeriesOrder.boughtDate,
        price: customSeriesOrder.bought,
      };
      const tradeOrder2 = {
        time: customSeriesOrder.soldDate,
        price: customSeriesOrder.sold,
      };
      const defaultOptions = {
        labelBackgroundColor: "rgba(0, 0, 0, 0.25)",
        labelTextColor: "rgb(255, 255, 255)",
        profitableTradeColor: "rgba(100, 200, 50, 0.5)",
        unprofitableTradeColor: "rgba(242, 54, 69, 0.5)",
        numberOfDigitsDecimalPrice: 7,
      };
      const trend = new TradePlotChartPlugin(
        chart,
        candlestickSeries,
        tradeOrder1,
        tradeOrder2,
        defaultOptions
      );
      candlestickSeries.attachPrimitive(trend);
    });

    chart.timeScale().fitContent();
  }

  findClosestDate(
    targetDate: UTCTimestamp,
    dateArray: UTCTimestamp[]
  ): UTCTimestamp {
    if (dateArray.length === 0) {
      return null;
    }

    let closestDate = dateArray[0];
    let timeDiff = Math.abs(targetDate - closestDate);

    for (const date of dateArray) {
      const currentDiff = Math.abs(targetDate - date);
      if (currentDiff < timeDiff) {
        timeDiff = currentDiff;
        closestDate = date;
      }
    }

    return closestDate;
  }
}
