import { Component, OnInit, Inject } from '@angular/core';
import { IOrderService } from '../../interfaces/order-service';
import { Order } from '../../model/order';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';
import { AbstractPage } from '../abstract-page/abstract-page';

@Component({
  selector: 'app-orders-page',
  templateUrl: './orders-page.component.html',
  styleUrls: ['./orders-page.component.css']
})
export class OrdersPageComponent extends AbstractPage implements OnInit {

  constructor( @Inject('OrderService') private orderService: IOrderService, private _sanitizer: DomSanitizer) {
    super();
  }

  ngOnInit() {
    this.setLoading(true);
    let self = this;
    this.orderService.updateOrders().subscribe(x => self.setLoading(false), 
    error=> {
      self.setLoading(false);
      alert("Error in loading orders");
    });
  }

  getOrders(): Order[] {
    return this.orderService.getOrders();
  }

  getWidth(width) {
    return this._sanitizer.bypassSecurityTrustStyle(width / 100 + '%');
  }

}
