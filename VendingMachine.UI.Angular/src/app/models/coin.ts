import { TypeCoin } from './enums';

export class Coin {
  typeCoin: TypeCoin;
  price = TypeCoin[this.typeCoin];

  constructor(typeCoin: TypeCoin) {
    this.typeCoin = typeCoin;
  }
}
