import { Injectable } from '@angular/core';
import { Coin } from '../models/coin';
import { Observable, throwError } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Product } from '../models/product';
import { CreatorProduct } from '../models/creator';
import { catchError } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {

  private apiServiceUrl = 'https://localhost:5001/api/Payment/';

  constructor(private http: HttpClient, private toastr: ToastrService) { }

  public pushAmountDeposited(coin: Coin) {
    return this.http.post(this.apiServiceUrl + 'AddAmountDeposit', coin)
    .pipe(
      catchError((e) => this.handleError(e, this.toastr))
    );
  }

  public getSurrenderUser(): Observable<Array<Coin>> {
    return this.http.put<Array<Coin>>(this.apiServiceUrl + 'GetDepositCustomer', {}).pipe(
      catchError((e) => this.handleError(e, this.toastr))
    );
  }

  public buyProduct(creatorProduct: CreatorProduct): Observable<Product> {
    return this.http.post<Product>(this.apiServiceUrl + 'BuyProduct', creatorProduct).pipe(
      catchError((e) => this.handleError(e, this.toastr))
    );
  }

  private handleError(error: HttpErrorResponse, toastr: ToastrService) {
    if (error.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error.message);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);

      toastr.error('Error!', error.error);

      if (error.error) {
        return throwError (error.error);
      }
    }
    // return an observable with a user-facing error message
    return throwError (
      'Something bad happened; please try again later.');
  }
}
