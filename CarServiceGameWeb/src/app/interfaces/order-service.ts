import { Order } from "../model/order";

export interface IOrderService {
    
    updateOrders();
    getOrders(): Array<Order>;
}