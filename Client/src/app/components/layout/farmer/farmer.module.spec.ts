import { FarmerModule } from './farmer.module';

describe('FarmerModule', () => {
  let farmerModule: FarmerModule;

  beforeEach(() => {
    farmerModule = new FarmerModule();
  });

  it('should create an instance', () => {
    expect(farmerModule).toBeTruthy();
  });
});
