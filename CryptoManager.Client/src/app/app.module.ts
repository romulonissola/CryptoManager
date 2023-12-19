import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import {
  HttpClientModule,
  HttpClient,
  HTTP_INTERCEPTORS,
} from "@angular/common/http";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { FormsModule } from "@angular/forms";
import { LoaderComponent } from "./components/loader/loader.component";

import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import {
  ApiService,
  AuthGuard,
  JwtService,
  AccountService,
  ExchangeService,
  AssetService,
  OrderService,
  SetupTraderService,
  JwtHelper,
  HttpErrorInterceptor,
  HTTPStatus,
  HealthService,
  AlertHandlerService,
  BackTestSetupTraderService,
  SharedPipesModule,
} from "./shared";

const Interceptors_Services = [HttpErrorInterceptor, HTTPStatus];
// AoT requires an exported function for factories
export function createTranslateLoader(http: HttpClient) {
  // for development
  // return new TranslateHttpLoader(http, '/start-angular/SB-Admin-BS4-Angular-5/master/dist/assets/i18n/', '.json');
  return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}

@NgModule({
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: createTranslateLoader,
        deps: [HttpClient],
      },
    }),
    AppRoutingModule,
    FormsModule,
    SharedPipesModule,
  ],
  declarations: [AppComponent, LoaderComponent],
  providers: [
    ApiService,
    AuthGuard,
    JwtService,
    AccountService,
    ExchangeService,
    AssetService,
    OrderService,
    SetupTraderService,
    BackTestSetupTraderService,
    JwtHelper,
    HealthService,
    AlertHandlerService,
    Interceptors_Services,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
