import { TypeCoin } from './enums';

export class Coin {
  typeCoin: TypeCoin;

  public price = () => {
    return TypeCoin[this.typeCoin];
  }

  constructor(typeCoin: TypeCoin) {
    this.typeCoin = typeCoin;
  }
}
