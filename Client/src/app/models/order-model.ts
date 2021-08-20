import { OrderStatus } from "./orderStatus-model";
import { Product } from "./product-model";

export class Orders {
  id: Number;
  status:OrderStatus;
  products:Product;
}
