import { TestBed, async, inject } from '@angular/core/testing';

import { HumanresourcesRoleGuard } from './humanresources-role.guard';

describe('HumanresourcesRoleGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [HumanresourcesRoleGuard]
    });
  });

  it('should ...', inject([HumanresourcesRoleGuard], (guard: HumanresourcesRoleGuard) => {
    expect(guard).toBeTruthy();
  }));
});
