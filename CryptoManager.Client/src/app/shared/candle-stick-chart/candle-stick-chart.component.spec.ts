import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CandleStickChartComponent } from './candle-stick-chart.component';

describe('CandleStickChartComponent', () => {
  let component: CandleStickChartComponent;
  let fixture: ComponentFixture<CandleStickChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CandleStickChartComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CandleStickChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
