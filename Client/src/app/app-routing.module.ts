import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './Account/login/login.component';
import { RegisterComponent } from './Account/register/register.component';
import { HomeComponent } from './home/home.component';
import { OrderListComponent } from './Order/order-list/order-list.component';
import { ProductListComponent } from './Product/product-list/product-list.component';

const routes: Routes = [
  {path:'', component : HomeComponent ,pathMatch: 'full' },
  {path:'Products', component : ProductListComponent  },
  {path:'Orders', component : OrderListComponent  },
  {path:'Login', component : LoginComponent },
  {path:'Register', component : RegisterComponent }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
exports: [RouterModule]
})
export class AppRoutingModule { }
