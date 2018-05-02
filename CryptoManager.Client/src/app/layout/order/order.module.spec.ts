import { OrderModule } from './order.module';

describe('OrderModule', () => {
    let module: OrderModule;

    beforeEach(() => {
        module = new OrderModule();
    });

    it('should create an instance', () => {
        expect(module).toBeTruthy();
    });
});
