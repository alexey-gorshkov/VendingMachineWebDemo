import { Component, OnInit } from '@angular/core';
import { VendingMachineState } from 'src/app/models/vending-machine-state';
import { VendingMachineService } from 'src/app/services/vending-machine.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  // current state VM
  vendingMachineState: VendingMachineState;
  constructor(private apiService: VendingMachineService,
              private router: Router) { }

  ngOnInit() {
    this.initializeVM();
  }

  public initializeVM() {
    this.apiService.initializeVM()
      .subscribe(result => {
        this.vendingMachineState = result;
    });
  }

  public logout() {
    localStorage.removeItem('isLoggedIn');
    localStorage.removeItem('token');
    localStorage.removeItem('expiresDate');
    this.router.navigate(['/login']);
  }

  public refreshDataVM(event: any) {
    this.initializeVM();
  }
}
