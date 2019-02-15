import { Component, OnInit } from '@angular/core';
import { VendingMachine } from 'src/app/models/vending-machine';
import { Product } from 'src/app/models/product';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  // состояние машины
  vendingMachine: VendingMachine;

  // покупки пользователя
  userProducts: Array<Product> = [];

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.initializeVM();
  }

  public initializeVM() {
    this.userProducts = [];
    this.apiService.initializeVM()
      .subscribe(result => {
        this.vendingMachine = result;
    });
  }

}
