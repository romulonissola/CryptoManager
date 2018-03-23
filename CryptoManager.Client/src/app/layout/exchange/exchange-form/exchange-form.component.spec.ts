import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExchangeFormComponent } from './exchange-form.component';

describe('ExchangeFormComponent', () => {
  let component: ExchangeFormComponent;
  let fixture: ComponentFixture<ExchangeFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExchangeFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExchangeFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
