import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TokenResult } from '../models/tokenResult';
import { ILogin } from '../models/login';
import { BaseResult } from '../models/baseResult';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiServiceUrl = 'https://localhost:5001/api/auth/';

  constructor(private http: HttpClient) { }

  getToken(model: ILogin) {
    return this.http.post<TokenResult>(this.apiServiceUrl + 'login', model);
  }
  getTokenLocal() {
    return localStorage.getItem('token');
  }

  registerUser(model: ILogin) {
    return this.http.post<BaseResult>(this.apiServiceUrl + 'register', model);
  }

  logout() {
    localStorage.setItem('isLoggedIn', 'false');
    localStorage.removeItem('token');
  }
}
