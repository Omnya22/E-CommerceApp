import { Component, OnInit } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {

  constructor(private service:ProductService) { }


  ProductList:any=[];
  ProductListWithoutFilter:any=[];

  ModalTitle:string;
  ActivateAddEdit:boolean=false;
  ActivateDetail:boolean=false;
  Detail:boolean=false;
  PhotoPath="https://localhost:44382/Photos/";

  product:any;

  ngOnInit(): void {
    this.refreshProductList();
  }

  addClick(){
    this.product={
      id:0,
      name:"",
      description:"",
      price:0,
      PhotoUrl:"unknownProduct.jpeg"
    }
    this.ModalTitle="Add Product";
    this.ActivateAddEdit=true;
    this.ActivateDetail=false;
    this.Detail=false;
    this.PhotoPath=this.PhotoPath+this.product.PhotoUrl
  }

  editClick(item){
    this.product=item;
    this.product={
      id:item.id,
      name:item.name,
      description:item.description,
      price:item.price,
      PhotoUrl:item.photoUrl,
      PhotoPath:this.PhotoPath+item.photoUrl
    }
    this.ModalTitle="Edit Product";
    this.ActivateAddEdit=true;
    this.ActivateDetail=false;
    this.Detail=false;
  }

  deleteClick(item){
    if(confirm('Are you sure??')){
      this.service.deleteProduct(item.id).subscribe(data=>{
        alert(data.toString());
        this.refreshProductList();
      })
    }
  }

  closeClick(){
    this.ActivateAddEdit=false;
    this.refreshProductList();
  }


  refreshProductList(){
    this.service.getProductList().subscribe(data=>{
      this.ProductList = this.ProductListWithoutFilter=data;
    });
  }

}

