import { Component, OnInit, Input } from '@angular/core';
import { Product } from 'src/app/models/product';

@Component({
  selector: 'app-customer-product-list',
  templateUrl: './customer-product-list.component.html',
  styleUrls: ['./customer-product-list.component.css']
})
export class CustomerProductListComponent implements OnInit {

  @Input() userProducts: Product[];

  constructor() { }

  ngOnInit() {
  }
}
