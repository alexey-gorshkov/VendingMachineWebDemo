import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VendingMachineProductListComponent } from './vending-machine-product-list.component';

describe('VendingMachineProductListComponent', () => {
  let component: VendingMachineProductListComponent;
  let fixture: ComponentFixture<VendingMachineProductListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VendingMachineProductListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendingMachineProductListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
