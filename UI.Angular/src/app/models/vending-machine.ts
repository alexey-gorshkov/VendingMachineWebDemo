import { Purse } from './purse';
import { CreatorProduct } from './creator-product';

export class VendingMachine {
  purseUser: Purse;
  purseVM: Purse;
  creators: Array<CreatorProduct>;
  amountDeposited: number;
}
