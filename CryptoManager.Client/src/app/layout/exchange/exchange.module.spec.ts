import { ExchangeModule } from './exchange.module';

describe('ExchangeModule', () => {
    let module: ExchangeModule;

    beforeEach(() => {
        module = new ExchangeModule();
    });

    it('should create an instance', () => {
        expect(module).toBeTruthy();
    });
});
