import { TestBed, async, inject } from '@angular/core/testing';

import { FarmerRoleGuard } from './farmer-role.guard';

describe('FarmerRoleGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FarmerRoleGuard]
    });
  });

  it('should ...', inject([FarmerRoleGuard], (guard: FarmerRoleGuard) => {
    expect(guard).toBeTruthy();
  }));
});
