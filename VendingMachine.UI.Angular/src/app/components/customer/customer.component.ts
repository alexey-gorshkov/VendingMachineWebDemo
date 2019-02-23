import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { VendingMachineService } from 'src/app/services/vending-machine.service';
import { Customer } from 'src/app/models/customer';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  // состояние машины
  @Input() customer: Customer;
  @Output() refreshDataVM: EventEmitter<any> = new EventEmitter<any>();

  constructor(private service: VendingMachineService) { }

  ngOnInit() {
  }

  public refreshData(event: any) {
    this.refreshDataVM.emit(null);
  }
}
