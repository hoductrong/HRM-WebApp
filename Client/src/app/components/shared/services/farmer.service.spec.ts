import { TestBed, inject } from '@angular/core/testing';

import { FarmerService } from './farmer.service';

describe('FarmerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [FarmerService]
    });
  });

  it('should be created', inject([FarmerService], (service: FarmerService) => {
    expect(service).toBeTruthy();
  }));
});
