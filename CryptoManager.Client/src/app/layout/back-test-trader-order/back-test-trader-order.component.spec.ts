import { ComponentFixture, TestBed } from "@angular/core/testing";

import { BackTestTraderOrderComponent } from "./back-test-trader-order.component";

describe("BackTestTraderOrderComponent", () => {
  let component: BackTestTraderOrderComponent;
  let fixture: ComponentFixture<BackTestTraderOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BackTestTraderOrderComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BackTestTraderOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
