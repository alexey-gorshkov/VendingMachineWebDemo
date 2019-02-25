
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VendingMachineState } from '../models/vending-machine-state';
import { plainToClass } from 'class-transformer';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VendingMachineService {

  private apiServiceUrl = 'https://localhost:44341/api/VendingMachine/';

  constructor(private http: HttpClient) { }

  public initializeVM(): Observable<VendingMachineState> {
    return this.http.get(this.apiServiceUrl).pipe(
      map(response => {
        return plainToClass(VendingMachineState, response);
      })
    );
  }
}
