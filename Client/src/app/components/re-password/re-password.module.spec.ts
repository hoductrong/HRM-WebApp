import { RePasswordModule } from './re-password.module';

describe('RePasswordModule', () => {
  let rePasswordModule: RePasswordModule;

  beforeEach(() => {
    rePasswordModule = new RePasswordModule();
  });

  it('should create an instance', () => {
    expect(rePasswordModule).toBeTruthy();
  });
});
