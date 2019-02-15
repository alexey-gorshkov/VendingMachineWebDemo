import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app.component';
import { CustomerPurseComponent } from './components/customer-purse/customer-purse.component';
import { PurseVendingMachineComponent } from './components/purse-vending-machine/purse-vending-machine.component';
import { CustomerProductListComponent } from './components/customer-product-list/customer-product-list.component';
import { VendingMachineComponent } from './components/vending-machine/vending-machine.component';
import { CustomerComponent } from './components/customer/customer.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { VendingMachineProductListComponent } from './components/vending-machine-product-list/vending-machine-product-list.component';
import { AuthPageComponent } from './components/auth-page/auth-page.component';
import { AuthGuard } from './auth.guard';
import { JwtInterceptorService } from './jwt.interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
    CustomerPurseComponent,
    PurseVendingMachineComponent,
    CustomerProductListComponent,
    VendingMachineComponent,
    CustomerComponent,
    HomePageComponent,
    VendingMachineProductListComponent,
    AuthPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    AuthGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
