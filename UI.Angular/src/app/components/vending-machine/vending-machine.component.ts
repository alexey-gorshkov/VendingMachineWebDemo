import { Component, OnInit, Input } from '@angular/core';
import { VendingMachine } from 'src/app/models/vending-machine';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-vending-machine',
  templateUrl: './vending-machine.component.html',
  styleUrls: ['./vending-machine.component.css']
})
export class VendingMachineComponent implements OnInit {
  // состояние машины
  @Input() vendingMachine: VendingMachine;

  constructor(private apiService: ApiService) { }

  ngOnInit() {
  }

}
