import { Purse } from './purse';
import { Type } from 'class-transformer';
import { Product } from './product';

export class Customer {
    amountDeposited: number;

    @Type(() => Purse)
    purse: Purse;

    @Type(() => Product)
    products: Product[];
}
