import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CustomerPurseComponent } from './customer-purse.component';

describe('CustomerPurseComponent', () => {
  let component: CustomerPurseComponent;
  let fixture: ComponentFixture<CustomerPurseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CustomerPurseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CustomerPurseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
