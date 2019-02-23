import { Purse } from './purse';
import { Type } from 'class-transformer';

export class Customer {
    amountDeposited: number;

    @Type(() => Purse)
    purseCustomer: Purse;
}
