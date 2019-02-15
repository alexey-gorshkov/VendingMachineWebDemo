import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PurseVendingMachineComponent } from './purse-vending-machine.component';

describe('PurseVendingMachineComponent', () => {
  let component: PurseVendingMachineComponent;
  let fixture: ComponentFixture<PurseVendingMachineComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PurseVendingMachineComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PurseVendingMachineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
