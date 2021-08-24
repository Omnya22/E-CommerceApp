import { Component, Input, OnInit } from '@angular/core';
import { OrderService } from './../../services/order.service';

@Component({
  selector: 'app-order-card',
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.css']
})
export class OrderCardComponent implements OnInit {

  constructor(private service:OrderService) { }

  @Input()
  product:any;
  Order:any;
  id:Number;
  isWaited:boolean;
  isRejected:boolean;
  isAccepted:boolean;
  orderId:Number;
  frmOrder:String;

  orderList:any=[];

  orderListWithoutFilter:any=[];

  ModalTitle:string;

  isAdmin:boolean=false;

  order:any;

  ngOnInit(): void {
    this.refreshOrderList();
    this.load();
  }

  refreshOrderList(){
    this.service.getOrderList().subscribe(data=>{
      this.orderList = this.orderListWithoutFilter=data;
      localStorage.getItem('email') =="admin@example.com" ? this.isAdmin = true : this.isAdmin = false;
    });
  }

  load(){
    this.Order = this.Order;
    this.product = this.product;
    this.id=this.id;
    this.isAccepted = this.isAccepted;
    this.isRejected = this.isRejected;
    this.isWaited = this.isWaited;
  }

  addOrder(){
  var val = {
      product:this.product,
      orderId:this.id,
      frmOrder:this.frmOrder,
      isAccepted:this.isAccepted,
      isWaited:this.isWaited,
      isRejected:this.isRejected
    };
    this.service.addOrder(val).subscribe(res=>{
      alert(res.toString());
    });
  }

  updateOrder(){
    var val = {
      product:this.product,
      id:this.order.id,
      isAccepted:this.order.isAccepted,
      isWaited:this.order.isWaited,
      isRejected:this.order.isRejected

    };
    console.log(val);
    this.service.updateOrder(val).subscribe(res=>{
    alert(res.toString());
    });
  }

  editClick(item){
    this.order=item;
    this.order={
      id:item.id,
      productId:item.orderProducts["productId"],
      isWaited:item.orderStatus.isWaited,
      isAccepted:item.orderStatus.isAccepted,
      isRejected:item.orderStatus.isRejected,
    }
    console.log(this.order);
    this.ModalTitle="Edit Order Status";
  }

  closeClick(){
    this.refreshOrderList();
  }

}
