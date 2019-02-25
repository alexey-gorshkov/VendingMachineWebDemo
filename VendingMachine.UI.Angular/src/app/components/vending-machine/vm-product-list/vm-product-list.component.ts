import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CreatorProduct } from 'src/app/models/creator';
import { PaymentService } from 'src/app/services/payment.service';
import { TypeProduct } from 'src/app/models/enums';
import { Product } from 'src/app/models/product';

@Component({
  selector: 'app-vm-product-list',
  templateUrl: './vm-product-list.component.html',
  styleUrls: ['./vm-product-list.component.css']
})
export class VMProductListComponent implements OnInit {
  @Input() creatorProducts: CreatorProduct[];
  @Output() refreshData: EventEmitter<any> = new EventEmitter<any>();

  constructor(private service: PaymentService) { }

  ngOnInit() {
  }

  // покупка товара
  public buyProduct(creatorProduct: CreatorProduct) {
    this.service.buyProduct(creatorProduct)
      .subscribe(result => {
        if (result != null) {
          this.refreshData.emit(null);
        }
    },
    error => { alert(error); }
    );
  }
}
