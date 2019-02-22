import { CoinCount } from './coin-count';

export class Purse {
  coins: Array<CoinCount>;

  public getSumCoins(): number {
    return this.coins.map(a => a.typeCoin * a.count).reduce((a, b): number => (a + b));
  }
}
