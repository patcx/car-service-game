import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HistoryOrdersComponent } from './history-orders.component';

describe('HistoryOrdersComponent', () => {
  let component: HistoryOrdersComponent;
  let fixture: ComponentFixture<HistoryOrdersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HistoryOrdersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HistoryOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
