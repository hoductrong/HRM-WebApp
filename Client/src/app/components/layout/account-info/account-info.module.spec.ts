import { AccountInfoModule } from './account-info.module';

describe('AccountInfoModule', () => {
  let accountInfoModule: AccountInfoModule;

  beforeEach(() => {
    accountInfoModule = new AccountInfoModule();
  });

  it('should create an instance', () => {
    expect(accountInfoModule).toBeTruthy();
  });
});
