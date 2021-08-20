import { Component, OnInit } from '@angular/core';
import { OrderService } from './../../services/order.service';

@Component({
  selector: 'app-order-card',
  templateUrl: './order-card.component.html',
  styleUrls: ['./order-card.component.css']
})
export class OrderCardComponent implements OnInit {

  constructor(private service:OrderService) { }


  orderList:any=[];
  orderListWithoutFilter:any=[];

  ModalTitle:string;
  ActivateAddEdit:boolean=false;
  ActivateDetail:boolean=false;
  Detail:boolean=false;

  order:any;

  ngOnInit(): void {
    this.refreshOrderList();
  }

  refreshOrderList(){
    this.service.getOrderList().subscribe(data=>{
      this.orderList = this.orderListWithoutFilter=data;
      console.log(this.orderList);

    });
  }

}
