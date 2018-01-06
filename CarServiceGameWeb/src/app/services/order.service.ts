import { Http, Headers } from '@angular/http';
import { Injectable } from '@angular/core';
import { IOrderService } from '../interfaces/order-service'
import { Order } from '../model/order'
import { AccountService } from './account.service';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { RepairProcess } from '../model/repair-process';

@Injectable()
export class OrderService implements IOrderService {

  appVersion: string = environment.appVersion;
  orders: Array<Order>;
  historyOrders: Array<RepairProcess>;

  constructor(private http: Http, private accountService: AccountService) { }

  updateOrders(): Observable<any> {
    let headers = this.accountService.getTokenHeader();
    if (headers == null) return;
    let self = this;
    return this.http.get(environment.url + `/api/v${this.appVersion}/Orders`, { headers: headers }).map(x => {
      self.createOrdersList(x.json())
    });
  }

  getOrders() {
    return this.orders;
  }

  createOrdersList(response) {
    this.orders = new Array();
    let self = this;
    response.forEach(element => {
      self.orders.push(new Order(element.RepairOrderId, element.CarName, element.RequiredWork, element.Reward, element.Description));
    });
  }

  getHistoryOrdersFromAPI(): Observable<any> {
    let headers = this.accountService.getTokenHeader();
    if (headers == null) return;
    let self = this;

    return new Observable(observer => {
      this.http.get(environment.url + `/api/v${this.appVersion}/Orders/History`, { headers: headers })
        .subscribe(x => {
          self.historyOrders = x.json()
          observer.next();
        })
    });
  }

  getHistoryOrders(): Array<RepairProcess> {
    return this.historyOrders;
  }

  completeOrder(orderId: string): Observable<any> {
    let headers = this.accountService.getTokenHeader();
    if (headers == null) return;
    headers.append("Content-Type", "application/x-www-form-urlencoded");
    let self = this;
    let content = `orderId=${orderId}`;
    return this.http.post(environment.url + `/api/v${this.appVersion}/Orders/Finish`, content, { headers: headers })
    .map(x => console.log(x));
  }

  immediatelyCompleteOrder(orderId: string) : Observable<any>{
    return null;
  }

  cancelOrder(orderId: string): Observable<any> {
    let headers = this.accountService.getTokenHeader();
    if (headers == null) return;
    headers.append("Content-Type", "application/x-www-form-urlencoded");
    let self = this;
    let content = `orderId=${orderId}`;
    return this.http.post(environment.url + `/api/v${this.appVersion}/Orders/Cancel`, content, { headers: headers })
    .map(x => console.log(x));
  }
}
