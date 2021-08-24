import { Component, Input, OnInit } from '@angular/core';
import { ProductService } from './../../services/product.service';
import { OrderService } from './../../services/order.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  constructor(private service:ProductService,private serviceOrder:OrderService) { }

  @Input()
  product:any;
  id:Number;
  name:String;
  description:String;
  orderId:Number;
  price:Number;
  frmProduct:String;
  PhotoUrl:String;
  PhotoPath="https://localhost:44382/Photos/";


  ProductList:any=[];
  ProductListWithoutFilter:any=[];

  ModalTitle:string;
  Detail:boolean=true;
  ActivateDetail:boolean=true;
  ActivateAddEdit:boolean=false;
  isRegistered:boolean;
  ngOnInit(): void {
    this.refreshProductList();
    this.load();
  }

  load(){
    this.product = this.product;
    this.id=this.product.id;
    this.price=this.product.price;
    this.name = this.product.name;
    this.description = this.product.description;
    this.PhotoUrl = this.product.PhotoUrl;
    this.ActivateDetail=true;
    this.Detail=true;
    this.ActivateAddEdit=false;
  }

  refreshProductList(){
    this.isUserRegistered();
    this.service.getProductList().subscribe(data=>{
      this.ProductList = this.ProductListWithoutFilter=data;
    });
  }

  detailClick(item){
    this.product=item;
    this.product={
      id:item.id,
      name:item.name,
      description:item.description,
      price:item.price,
      PhotoUrl:item.photoUrl,
      PhotoPath:this.PhotoPath
      }
    this.ModalTitle="Detail Product";
    this.ActivateDetail=true;
    this.Detail=true;
    this.ActivateAddEdit=false;

  }

  closeClick(){
    this.ActivateDetail=false;
    this.Detail=false;
    this.ActivateAddEdit=true;
    this.refreshProductList();
  }

  addOrder(Item){
    var val = {
      orderProducts:[{productId:Item.id}]
    };
    this.serviceOrder.addOrder(val).subscribe(res=>{
      alert(res.toString());
    });
  }

  isUserRegistered() {
    const email = !!localStorage.getItem('email');
    if (email) this.isRegistered = true;
    else       this.isRegistered = false;
  }

}
