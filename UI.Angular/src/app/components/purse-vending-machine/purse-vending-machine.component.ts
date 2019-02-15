import { Component, OnInit, Input } from '@angular/core';
import { Purse } from 'src/app/models/purse';

@Component({
  selector: 'app-purse-vending-machine',
  templateUrl: './purse-vending-machine.component.html',
  styleUrls: ['./purse-vending-machine.component.css']
})
export class PurseVendingMachineComponent implements OnInit {

  @Input() purseVM: Purse;

  constructor() { }

  ngOnInit() {
  }

}
