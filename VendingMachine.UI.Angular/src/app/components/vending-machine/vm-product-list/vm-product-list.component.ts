import { VendingMachineService } from 'src/app/services/vending-machine.service';
import { CreatorProduct } from 'src/app/models/creator-product';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-vm-product-list',
  templateUrl: './vm-product-list.component.html',
  styleUrls: ['./vm-product-list.component.css']
})
export class VMProductListComponent implements OnInit {

  @Input() creators: CreatorProduct[];

  constructor(private apiService: VendingMachineService) { }

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
