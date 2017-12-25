import { LandModule } from './land.module';

describe('LandModule', () => {
  let landModule: LandModule;

  beforeEach(() => {
    landModule = new LandModule();
  });

  it('should create an instance', () => {
    expect(landModule).toBeTruthy();
  });
});
