import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HamperByCatsComponent } from './hamper-by-cats.component';

describe('HamperByCatsComponent', () => {
  let component: HamperByCatsComponent;
  let fixture: ComponentFixture<HamperByCatsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HamperByCatsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HamperByCatsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
