import { Customer } from './customer';
import { VendingMachine } from './vending-machine';
import { Type } from 'class-transformer';

export class VendingMachineState {
  @Type(() => Customer)
  customer: Customer;

  @Type(() => VendingMachine)
  vendingMachine: VendingMachine;
}
