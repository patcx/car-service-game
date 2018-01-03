import { Observable } from "rxjs/Observable";
import { Order } from "../model/order";

export interface IOrderService {
    getHistoryOrders();
    updateOrders();
    getOrders(): Array<Order>;
}