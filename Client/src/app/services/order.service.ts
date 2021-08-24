import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  readonly APIUrl = "https://localhost:44382/Orders/";

  constructor(private http:HttpClient) { }

  getOrderList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'GetAllOrders');
  }
  getOrder(val:any):Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'GetOrder');
  }
  addOrder(val:any){
    return this.http.post(this.APIUrl+'AddOrder/',val);
  }

  updateOrder(val:any){
    return this.http.put(this.APIUrl+'UpdateOrder',val);
  }

  deleteOrder(val:any){
    return this.http.delete(this.APIUrl+'DeleteOrder/'+val);
  }

}
