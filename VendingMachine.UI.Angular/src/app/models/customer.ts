import { Purse } from './purse';
import { Type } from 'class-transformer';
import { CustomerProduct } from './customer-product';

export class Customer {
    amountDeposited: number;

    @Type(() => Purse)
    purse: Purse;

    @Type(() => CustomerProduct)
    products: CustomerProduct[];
}
