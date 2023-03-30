import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoboTraderOrderComponent } from './robo-trader-order.component';

describe('RoboTraderOrderComponent', () => {
  let component: RoboTraderOrderComponent;
  let fixture: ComponentFixture<RoboTraderOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoboTraderOrderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoboTraderOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
