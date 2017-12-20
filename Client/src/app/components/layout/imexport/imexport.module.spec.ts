import { ImexportModule } from './imexport.module';

describe('ImexportModule', () => {
  let imexportModule: ImexportModule;

  beforeEach(() => {
    imexportModule = new ImexportModule();
  });

  it('should create an instance', () => {
    expect(imexportModule).toBeTruthy();
  });
});
