import { Purse } from './purse';
import { CreatorProduct } from './creator-product';
import { Type } from 'class-transformer';

export class VendingMachine {
    @Type(() => Purse)
    purseVM: Purse;

    creators: Array<CreatorProduct>;
}
