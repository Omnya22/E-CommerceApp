import { Component, Input, OnInit } from '@angular/core';
import { OrderService } from './../../services/order.service';

@Component({
  selector: 'app-add-order',
  templateUrl: './add-order.component.html',
  styleUrls: ['./add-order.component.css']
})
export class AddOrderComponent implements OnInit {

  constructor(private service:OrderService) { }

  @Input()
  product:any;
  Order:any;
  id:Number;
  isWaiting:boolean;
  isRejected:boolean;
  isAccept:boolean;
  orderId:Number;
  frmOrder:String;

  ngOnInit(): void {
    this.load();
  }

  load(){
    this.Order = this.Order;
    this.product = this.product;
    this.id=this.id;
    this.isAccept = this.isAccept;
    this.isRejected = this.isRejected;
    this.isWaiting = this.isWaiting;
  }

  addOrder(){
  var val = {
      product:this.product,
      orderId:this.id,
      frmOrder:this.frmOrder,
      isAccept:this.isAccept,
      isWaiting:this.isWaiting,
      isRejected:this.isRejected
    };
    this.service.addOrder(val).subscribe(res=>{
      alert(res.toString());
    });
  }

  updateOrder(){
    var val = {
      product:this.product,
      orderId:this.id,
      isAccept:this.isAccept,
      isWaiting:this.isWaiting,
      isRejected:this.isRejected
    };
    console.log(val);
    this.service.updateOrder(val).subscribe(res=>{
    alert(res.toString());
    });
  }

}
