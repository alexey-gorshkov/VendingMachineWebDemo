import { Injectable } from '@angular/core';
import { Coin } from '../models/coin';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CustomerDepositService {

  private apiServiceUrl = 'https://localhost:44341/api/CustomerDeposit/';

  constructor(private http: HttpClient) { }

  public pushAmountDeposited(coin: Coin) {
    return this.http.post(this.apiServiceUrl, coin);
  }

  public getSurrenderUser(): Observable<Array<Coin>> {
    return this.http.delete<Array<Coin>>(this.apiServiceUrl, {});
  }
}
