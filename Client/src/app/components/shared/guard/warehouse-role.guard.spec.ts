import { TestBed, async, inject } from '@angular/core/testing';

import { WarehouseRoleGuard } from './warehouse-role.guard';

describe('WarehouseRoleGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [WarehouseRoleGuard]
    });
  });

  it('should ...', inject([WarehouseRoleGuard], (guard: WarehouseRoleGuard) => {
    expect(guard).toBeTruthy();
  }));
});
