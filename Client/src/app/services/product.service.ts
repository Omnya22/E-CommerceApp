import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  readonly APIUrl = "https://localhost:44382/Products/";
  readonly PhotoUrl ="https://localhost:44382/Photos/";

  constructor(private http:HttpClient) { }

  getProductList():Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'GetAllProducts');
  }
  getProduct(val:any):Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'GetProduct');
  }
  addProduct(val:any){
    return this.http.post(this.APIUrl+'AddProduct/',val);
  }

  updateProduct(val:any){
    return this.http.put(this.APIUrl+'UpdateProduct',val);
  }

  ​
  deleteProduct(val:any){
    return this.http.delete(this.APIUrl+'DeleteProduct?id='+val);
  }

  UploadPhoto(val:any){
    return this.http.post(this.APIUrl+'SaveFile',val);
  }

}
