import { ApiService } from 'src/app/services/api.service';
import { CreatorProduct } from 'src/app/models/creator-product';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-vending-machine-product-list',
  templateUrl: './vending-machine-product-list.component.html',
  styleUrls: ['./vending-machine-product-list.component.css']
})
export class VendingMachineProductListComponent implements OnInit {

  @Input() creators: CreatorProduct[];

  constructor(private apiService: ApiService) { }

  ngOnInit() {
  }

  // покупка товара
  public onClickPay(creator: CreatorProduct) {

    // if (this.vendingMachine.AmountDeposited < creator.price) {
    //   alert('Недостаточно средств!');
    //   return;
    // }

    this.apiService.payProduct(creator.guidCreator)
      .subscribe(result => {
        if (result != null) {
          // this.userProducts.push(result);
          // this.refreshStatusVM();
        }
    });
  }
}
