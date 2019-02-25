import { TypeProduct } from './enums';
import { Product } from './product';
import { Type } from 'class-transformer';

export class CreatorProduct {
  availability: number;
  typeProduct: TypeProduct;

  @Type(() => Product)
  product: Product;
}
