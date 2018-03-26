import { AssetModule } from './asset.module';

describe('AssetModule', () => {
    let module: AssetModule;

    beforeEach(() => {
        module = new AssetModule();
    });

    it('should create an instance', () => {
        expect(module).toBeTruthy();
    });
});
