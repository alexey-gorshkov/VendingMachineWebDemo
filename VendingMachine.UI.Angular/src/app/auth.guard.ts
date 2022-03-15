import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    const url: string = state.url;
    return this.verifyLogin(url);
  }

  verifyLogin(url: string): boolean {
    if (!this.isLoggedIn()) {
      this.router.navigate(['/login']);
      return false;
    }
    return this.isLoggedIn();
  }

  public isLoggedIn(): boolean {
    const status = localStorage.getItem('isLoggedIn') === 'true' && this.validateExpDateToken();
    return status;
  }

  private validateExpDateToken(): boolean {
    const expiresIn = localStorage.getItem('expiresDate');
    if (expiresIn === null || isNaN(Date.parse(expiresIn))) {
      return false;
    }

    const expDate: Date = new Date(expiresIn);
    return new Date().getTime() < expDate.getTime();
  }
}
