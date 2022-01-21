import { HealthModule } from './health.module';

describe('HealthModule', () => {
    let module: HealthModule;

    beforeEach(() => {
        module = new HealthModule();
    });

    it('should create an instance', () => {
        expect(module).toBeTruthy();
    });
});
