import { Purse } from './purse';
import { CreatorProduct } from './creator-product';
import { Serializable } from './serializable';

export class VendingMachine extends Serializable {
  purseUser: Purse;
  purseVM: Purse;
  creators: Array<CreatorProduct>;
  amountDeposited: number;
}
