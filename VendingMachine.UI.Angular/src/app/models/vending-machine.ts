import { Purse } from './purse';
import { Type } from 'class-transformer';
import { CreatorProduct } from './creator';

export class VendingMachine {
    @Type(() => Purse)
    purse: Purse;

    @Type(() => CreatorProduct)
    creatorProducts: Array<CreatorProduct>;
}
