import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TokenResult } from '../models/tokenResult';
import { RegisterResult } from '../models/registerResult';
import { ILogin } from '../models/login';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiServiceUrl = 'https://localhost:44341/api/auth/';

  constructor(private http: HttpClient) { }

  getToken(model: ILogin) {
    return this.http.post<TokenResult>(this.apiServiceUrl + 'login', model);
  }
  getTokenLocal() {
    return localStorage.getItem('token');
  }

  registerUser(model: ILogin) {
    return this.http.post<RegisterResult>(this.apiServiceUrl + 'register', model);
  }

  logout() {
    localStorage.setItem('isLoggedIn', 'false');
    localStorage.removeItem('token');
  }
}
