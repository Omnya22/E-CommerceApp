import { Component, Input, OnInit } from '@angular/core';
import { Product } from 'src/app/models/product-model';
import { ProductService } from './../../services/product.service';
import { Orders } from './../../models/order-model';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {


  constructor(private service:ProductService) { }

  @Input()product:Product;
  order:Orders;
  id:Number;
  name:String;
  description:String;
  orderId:Number;
  price:Number;
  frmProduct:String;
  PhotoName:String;
  PhotoPath:String;
  Detail:boolean=false;

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.id=this.product.id;
    this.price=this.product.price;
    this.name = this.product.name;
    this.description = this.product.description;
    this.PhotoName = this.product.PhotoName;
    this.PhotoPath=this.service.PhotoUrl+this.PhotoName;
    this.Detail = this.Detail = true;
  }

  addProduct(){
  var val = {
      Id:this.id,
      Name:this.name,
      Description:this.description,
      Price:this.price,
      PhotoName:this.PhotoName,
      frmProduct:this.frmProduct,
      Detail:this.Detail = true
    };
    this.service.addProduct(val).subscribe(res=>{
      alert(res.toString());
    });
  }

  updateProduct(){
    var val = {
      Id:this.id,
      Name:this.name,
      Description:this.description,
      Price:this.price,
      PhotoName:this.PhotoName,
      frmProduct:this.frmProduct,
      Detail:this.Detail = true
    };

    this.service.updateProduct(val).subscribe(res=>{
    alert(res.toString());
    });
  }


  uploadPhoto(event){
    var file=event.target.files[0];
    const formData:FormData=new FormData();
    formData.append('uploadedFile',file,file.name);

    this.service.UploadPhoto(formData).subscribe((data:any)=>{
      this.PhotoName=data.toString();
      this.PhotoPath=this.service.PhotoUrl+this.PhotoName;
    })
  }
}
