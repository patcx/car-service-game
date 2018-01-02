import { Injectable } from '@angular/core';
import { IOfficeService } from '../interfaces/office-service';
import { TokenService } from './token.service';
import { Http } from '@angular/http';
import { environment } from '../../environments/environment';

@Injectable()
export class OfficeService implements IOfficeService {

  private appVersion = environment.appVersion;
  private garageBalance: number;

  constructor(private http: Http, private tokenService: TokenService) { }

  setCashBalance(balance) {
    this.garageBalance = balance;
  }

  getCashBalance() :number {
    return this.garageBalance;
  }

  getGarageBalance() {
    let headers = this.tokenService.getTokenHeader();
    if (headers == null) return;
    let self = this;
    this.http.get(environment.url + `/api/v${this.appVersion}/Garage/Balance`, { headers: headers })
      .subscribe(x => self.setCashBalance(x.json().balance));
  }

  updateGarage() {
  }
  getHistoryOrders() {
  }
}
