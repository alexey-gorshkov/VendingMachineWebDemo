import { Component, OnInit } from '@angular/core';
import { VendingMachineState } from 'src/app/models/vending-machine-state';
import { VendingMachineService } from 'src/app/services/vending-machine.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  // current state VM
  vendingMachineState: VendingMachineState;
  constructor(private apiService: VendingMachineService) { }

  ngOnInit() {
    this.initializeVM();
  }

  public initializeVM() {
    this.apiService.initializeVM()
      .subscribe(result => {
        this.vendingMachineState = result;
    });
  }

  public refreshDataVM(event: any) {
    this.initializeVM();
  }
}
