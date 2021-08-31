import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { BsComponentComponent } from './bs-component.component';

describe('BsComponentComponent', () => {
    let component: BsComponentComponent;
    let fixture: ComponentFixture<BsComponentComponent>;

    beforeEach(
        waitForAsync(() => {
            TestBed.configureTestingModule({
                declarations: [BsComponentComponent]
            }).compileComponents();
        })
    );

    beforeEach(() => {
        fixture = TestBed.createComponent(BsComponentComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
