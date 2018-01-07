import { Observable } from "rxjs/Observable";
import { Order } from "../model/order";
import { RepairProcess } from "../model/repair-process";

export interface IOrderService {
    getHistoryOrders(): Array<RepairProcess>;
    getHistoryOrdersFromAPI(): Observable<any>;
    updateOrders();
    getOrders(): Array<Order>;
    completeOrder(orderId: string): Observable<any>;
    immediatelyCompleteOrder(orderId: string): Observable<any>;
    cancelOrder(orderId: string): Observable<any>;
    asignOrder(orderId, workerId, stallNumber): Observable<any>;
}