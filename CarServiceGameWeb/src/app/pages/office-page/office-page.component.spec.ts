import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OfficePageComponent } from './office-page.component';

describe('OfficePageComponent', () => {
  let component: OfficePageComponent;
  let fixture: ComponentFixture<OfficePageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OfficePageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OfficePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
