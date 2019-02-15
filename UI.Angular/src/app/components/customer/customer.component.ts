import { Component, OnInit, Input } from '@angular/core';
import { VendingMachine } from 'src/app/models/vending-machine';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrls: ['./customer.component.css']
})
export class CustomerComponent implements OnInit {
  // состояние машины
  @Input() vendingMachine: VendingMachine;

  constructor() { }

  ngOnInit() {
  }

}
