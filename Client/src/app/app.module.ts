import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './Account/login/login.component';
import { RegisterComponent } from './Account/register/register.component';
import { NavFooterComponent } from './nav-footer/nav-footer.component';
import { HomeComponent } from './home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from 'src/app/services/auth.service';
import { RegisterService } from './services/register.service';
import { ProductCardComponent } from './Product/product-card/product-card.component';
import { ProductDetailComponent } from './Product/product-detail/product-detail.component';
import { ProductListComponent } from './Product/product-list/product-list.component';
import { AddProductComponent } from './Product/add-product/add-product.component';
import { OrderCardComponent } from './Order/order-card/order-card.component';
import { OrderDetailComponent } from './Order/order-detail/order-detail.component';
import { OrderListComponent } from './Order/order-list/order-list.component';
import { AddOrderComponent } from './Order/add-order/add-order.component';
import { ProductService } from 'src/app/services/product.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    RegisterComponent,
    NavFooterComponent,
    HomeComponent,
    ProductCardComponent,
    ProductDetailComponent,
    ProductListComponent,
    AddProductComponent,
    OrderCardComponent,
    OrderDetailComponent,
    OrderListComponent,
    AddOrderComponent
  ],
  imports: [
  BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [AuthService,RegisterService,ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
