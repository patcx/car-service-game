import { Injectable } from '@angular/core';
import { Http, Headers } from '@angular/http';
import { AccountService } from './account.service';
import { environment } from '../../environments/environment';
import {IPaymentService} from '../interfaces/payment-service'

@Injectable()
export class PaymentService implements IPaymentService {
  
  appVersion: string = environment.appVersion;

  constructor(private http: Http, private accountService: AccountService) {
  }

  sendCode(email: any) {
    let headers = this.accountService.getTokenHeader();
        if (headers == null) return;
        headers.append("Content-Type", "application/x-www-form-urlencoded");
        let self = this;
        let content = `email=${email}`;
        return this.http.post(environment.url + `/api/v${this.appVersion}/Payment/Send`, content, { headers: headers}).map(x=>x.json());
  }
  useCode(code: any) {
    let headers = this.accountService.getTokenHeader();
        if (headers == null) return;
        headers.append("Content-Type", "application/x-www-form-urlencoded");
        let self = this;
        let content = `code=${code}`;
        return this.http.post(environment.url + `/api/v${this.appVersion}/Payment/Process`, content, { headers: headers}).map(x=>x.json());
  }

}
