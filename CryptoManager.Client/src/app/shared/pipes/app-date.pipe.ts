import { Injectable, Pipe, PipeTransform } from "@angular/core";

@Pipe({
  name: "appDate",
})
@Injectable({
  providedIn: "root",
})
export class AppDatePipe implements PipeTransform {
  transform(
    value: Date | string | null | undefined,
    showTime = false
  ): string | null {
    if (!value) {
      return "";
    }
    var browserLanguage = navigator.language;
    const date: Date = typeof value === "string" ? new Date(value) : value;
    return showTime
      ? date.toLocaleString(browserLanguage)
      : date.toLocaleDateString(browserLanguage);
  }
}
