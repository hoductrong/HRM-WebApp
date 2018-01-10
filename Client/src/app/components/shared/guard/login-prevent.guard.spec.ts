import { TestBed, async, inject } from '@angular/core/testing';

import { LoginPreventGuard } from './login-prevent.guard';

describe('LoginPreventGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [LoginPreventGuard]
    });
  });

  it('should ...', inject([LoginPreventGuard], (guard: LoginPreventGuard) => {
    expect(guard).toBeTruthy();
  }));
});
