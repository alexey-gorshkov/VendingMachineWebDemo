import { TestBed } from '@angular/core/testing';

import { Jwt.InterceptorService } from './jwt.interceptor.service';

describe('Jwt.InterceptorService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: Jwt.InterceptorService = TestBed.get(Jwt.InterceptorService);
    expect(service).toBeTruthy();
  });
});
