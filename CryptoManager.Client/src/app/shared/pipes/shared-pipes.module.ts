import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AppDatePipe } from "./app-date.pipe";

@NgModule({
  imports: [CommonModule],
  declarations: [AppDatePipe],
  providers: [AppDatePipe],
  exports: [AppDatePipe],
})
export class SharedPipesModule {}
