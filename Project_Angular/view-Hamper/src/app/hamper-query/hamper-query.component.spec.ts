import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HamperQueryComponent } from './hamper-query.component';

describe('HamperQueryComponent', () => {
  let component: HamperQueryComponent;
  let fixture: ComponentFixture<HamperQueryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HamperQueryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HamperQueryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
