import { Injectable } from '@angular/core';
import {IOrderService} from '../interfaces/order-service'
import {Order} from '../model/order'
import { Http, Headers } from '@angular/http';
import { TokenService } from './token.service';
import { environment } from '../../environments/environment';

@Injectable()
export class OrderService implements IOrderService{

  appVersion: string = environment.appVersion;
  orders: Array<Order>;

  constructor(private http : Http, private tokenService : TokenService) { }

  updateOrders() {
    let headers = this.tokenService.getTokenHeader();
    if (headers == null) return;
    let self = this;
    this.http.get(environment.url + `/api/v${this.appVersion}/Orders`, { headers: headers }).subscribe(x => self.createOrdersList(x.json()));
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

}
