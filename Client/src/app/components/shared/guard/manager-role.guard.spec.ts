import { TestBed, async, inject } from '@angular/core/testing';

import { ManagerRoleGuard } from './manager-role.guard';

describe('ManagerRoleGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ManagerRoleGuard]
    });
  });

  it('should ...', inject([ManagerRoleGuard], (guard: ManagerRoleGuard) => {
    expect(guard).toBeTruthy();
  }));
});
