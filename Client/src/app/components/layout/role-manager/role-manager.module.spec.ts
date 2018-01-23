import { RoleManagerModule } from './role-manager.module';

describe('RoleManagerModule', () => {
  let roleManagerModule: RoleManagerModule;

  beforeEach(() => {
    roleManagerModule = new RoleManagerModule();
  });

  it('should create an instance', () => {
    expect(roleManagerModule).toBeTruthy();
  });
});
