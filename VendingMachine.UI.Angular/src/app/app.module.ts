import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/app.component';
import { CustomerPurseComponent } from './components/customer/customer-purse/customer-purse.component';
import { VMPurseComponent } from './components/vending-machine/vm-purse/vm-purse.component';
import { CustomerProductListComponent } from './components/customer/customer-product-list/customer-product-list.component';
import { VendingMachineComponent } from './components/vending-machine/vending-machine.component';
import { CustomerComponent } from './components/customer/customer.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { VMProductListComponent } from './components/vending-machine/vm-product-list/vm-product-list.component';
import { AuthPageComponent } from './components/auth-page/auth-page.component';
import { AuthGuard } from './auth.guard';
import { JwtInterceptorService } from './jwt.interceptor.service';
import { RegisterPageComponent } from './components/register-page/register-page.component';

import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    CustomerPurseComponent,
    VMPurseComponent,
    CustomerProductListComponent,
    VendingMachineComponent,
    CustomerComponent,
    HomePageComponent,
    VMProductListComponent,
    AuthPageComponent,
    RegisterPageComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot() // ToastrModule added
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
