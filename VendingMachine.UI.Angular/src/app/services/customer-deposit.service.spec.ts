import { TestBed } from '@angular/core/testing';

import { CustomerDepositService } from './customer-deposit.service';

describe('CustomerDepositService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CustomerDepositService = TestBed.get(CustomerDepositService);
    expect(service).toBeTruthy();
  });
});
