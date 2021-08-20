import { Component, OnInit } from '@angular/core';
import { ProductService } from './../../services/product.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent implements OnInit {

  constructor(private service:ProductService) { }

  ProductList:any=[];
  ProductListWithoutFilter:any=[];

  ModalTitle:string;
  Detail:boolean=false;
  ActivateDetail:boolean=false;
  product:any;

  ngOnInit(): void {
    this.refreshProductList();
  }
  refreshProductList(){
    this.service.getProductList().subscribe(data=>{
      this.ProductList = this.ProductListWithoutFilter=data;
    });
  }
  detailClick(item){
    console.log(item);
    this.product=item;
    this.Detail=true;
    this.product={
      id:item.id,
      name:item.name,
      description:item.description,
      price:item.price,
      PhotoName:item.PhotoName
      }
    console.log(this.product);
    this.ModalTitle="Detail Product";
    this.ActivateDetail=true;
  }

  closeClick(){
    this.ActivateDetail=false;
    this.refreshProductList();
  }
}
