import { Component, Input, OnInit } from '@angular/core';
import { ProductService } from './../../services/product.service';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {


  constructor(private service:ProductService) { }

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
  Detail:boolean=false;

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.product = this.product;
    this.id=this.product.id;
    this.price=this.product.price;
    this.name = this.product.name;
    this.description = this.product.description;
    this.PhotoUrl = this.product.PhotoUrl;
    this.Detail = this.Detail = true;
    console.log(this.PhotoPath);

    console.log(this.PhotoUrl);

  }

  addProduct(){
  var val = {
      Id:this.id,
      Name:this.name,
      Description:this.description,
      Price:this.price,
      photoUrl:this.PhotoUrl,
      frmProduct:this.frmProduct,
      Detail:this.Detail = true,
      PhotoPath:this.PhotoPath+this.PhotoUrl

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
      photoUrl:this.PhotoUrl,
      frmProduct:this.frmProduct,
      Detail:this.Detail = true,
      PhotoPath:this.PhotoPath+this.PhotoUrl
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
      this.PhotoUrl=data.toString();
    })
  }
}
