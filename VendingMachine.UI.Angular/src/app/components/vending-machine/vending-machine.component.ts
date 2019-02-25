import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { VendingMachine } from 'src/app/models/vending-machine';
import { VendingMachineService } from 'src/app/services/vending-machine.service';

@Component({
  selector: 'app-vending-machine',
  templateUrl: './vending-machine.component.html',
  styleUrls: ['./vending-machine.component.css']
})
export class VendingMachineComponent implements OnInit {
  // состояние машины
  @Input() vendingMachine: VendingMachine;
  @Output() refreshDataVM: EventEmitter<any> = new EventEmitter<any>();

  constructor(private apiService: VendingMachineService) { }

  ngOnInit() {
  }

  public refreshData(event: any) {
    this.refreshDataVM.emit(null);
  }
}
