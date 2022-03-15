import {
  Component,
  OnInit,
  Input,
  OnChanges,
  Output,
  EventEmitter,
} from '@angular/core';
import { Purse } from 'src/app/models/purse';
import { CoinCount } from 'src/app/models/coin-count';
import { Coin } from 'src/app/models/coin';
import { PaymentService } from 'src/app/services/payment.service';

@Component({
  selector: 'app-customer-purse',
  templateUrl: './customer-purse.component.html',
  styleUrls: ['./customer-purse.component.css'],
})
export class CustomerPurseComponent implements OnInit, OnChanges {
  @Input() purse?: Purse;
  @Input() amountDeposited = 0;
  @Output() refreshData: EventEmitter<any> = new EventEmitter<any>();

  constructor(private service: PaymentService) {}

  ngOnInit() {}

  public ngOnChanges(changes: any) {
    if ('purse' in changes) {
      // some code here
    }
  }

  // вносим монеты юзера
  public pushAmountDeposited(coinCount: CoinCount) {
    if (!coinCount.count) {
      return;
    }

    const newCoin = new Coin(coinCount.typeCoin);
    this.service.pushAmountDeposited(newCoin).subscribe(() => {
      this.refreshData.emit(null);
    });
  }

  public getSurrenderIsDisable() {
    return this.amountDeposited <= 0;
  }

  // запрашиваем сдачу юзера
  public getSurrenderAndHoldUser() {
    // вернуть сдачу через ответ сервера
    this.service.getSurrenderUser().subscribe((result) => {
      this.refreshData.emit(null);
    });
  }
}
