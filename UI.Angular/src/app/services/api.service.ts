
import { Injectable } from '@angular/core';
import { VendingMachine } from '../models/vending-machine';
import { Coin } from '../models/coin';
import { Product } from '../models/product';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private apiServiceUrl = 'https://localhost:44341/api/VendingMachine/';

  constructor(private http: HttpClient) { }

  public initializeVM(): Observable<VendingMachine> {
    return this.http.get<VendingMachine>(this.apiServiceUrl);
  }

  public pushAmmountDeposited(coin: Coin): Observable<number> {
    return this.http.post<number>(this.apiServiceUrl + 'PushAmmountDeposited', { coin });
  }

  public payProduct(guid: string): Observable<Product> {
    return this.http.post<Product>(this.apiServiceUrl + 'CreateProduct', { guid});
  }

  public refreshStatusVM(): Observable<VendingMachine> {
    return this.http.post<VendingMachine>(this.apiServiceUrl + 'RefreshStatus', {});
  }

  public getSurrenderUser(): Observable<Array<Coin>> {
    return this.http.post<Array<Coin>>(this.apiServiceUrl + 'GetSurrenderUser', {});
  }

  private getHeaders(): Headers {
    const headers = new Headers();
    headers.append('Content-Type', 'application/json');
    const authToken = localStorage.getItem('auth_token');
    headers.append('Authorization', `Bearer ${authToken}`);
    return headers;
  }
}
