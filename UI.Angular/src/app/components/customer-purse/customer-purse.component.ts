import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { Purse } from 'src/app/models/purse';
import { CoinCount } from 'src/app/models/coin-count';
import { Coin } from 'src/app/models/coin';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-customer-purse',
  templateUrl: './customer-purse.component.html',
  styleUrls: ['./customer-purse.component.css']
})
export class CustomerPurseComponent implements OnInit, OnChanges {

  @Input() purse: Purse;

  constructor(private apiService: ApiService) { }

  ngOnInit() {
  }

  public ngOnChanges(changes: any) {
    if ('purse' in changes) {
        // some code here
     }
  }

  // вносим монеты юзера
  public pushAmmountDeposited(coinCount: CoinCount) {
    if (!coinCount.count) {
      return;
    }

    const newCoin = new Coin(coinCount.typeCoin);

    this.apiService.pushAmmountDeposited(newCoin)
      .subscribe(result => {
        if (result != null) {
          // this.refreshStatusVM();
        }
      });
  }
}
