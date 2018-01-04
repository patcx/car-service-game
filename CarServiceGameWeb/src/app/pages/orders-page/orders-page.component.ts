import { Component, OnInit, Inject } from '@angular/core';
import { IOrderService } from '../../interfaces/order-service';
import { Order } from '../../model/order';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-orders-page',
  templateUrl: './orders-page.component.html',
  styleUrls: ['./orders-page.component.css']
})
export class OrdersPageComponent implements OnInit {

  constructor( @Inject('OrderService') private orderService: IOrderService, private _sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.orderService.updateOrders();
  }

  getOrders(): Order[] {
    return this.orderService.getOrders();
  }

  getWidth(width) {
    return this._sanitizer.bypassSecurityTrustStyle(width / 100 + '%');
  }

}
