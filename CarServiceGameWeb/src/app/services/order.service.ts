import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { TokenService } from './token.service';
import { Observable } from 'rxjs';
import { IOrderService } from '../interfaces/order-service';

@Injectable()
export class OrderService implements IOrderService {

  private appVersion = environment.appVersion;

  constructor(private http: Http, private tokenService: TokenService) { }

  getHistoryOrders() {
    let headers = this.tokenService.getTokenHeader();
    if (headers == null) return;
    let self = this;
    return new Observable(observer => {
      this.http.get(environment.url + `/api/v${this.appVersion}/Orders/History`, { headers: headers })
        .subscribe(x => observer.next(x.json()));
    });
  }
}
