import { TestBed } from '@angular/core/testing';

import { VendingMachineService } from './vending-machine.service';

describe('VendingMachineService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: VendingMachineService = TestBed.get(VendingMachineService);
    expect(service).toBeTruthy();
  });
});
