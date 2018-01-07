import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IGarageService } from '../interfaces/garage-service';
import { AccountService } from './account.service';

import 'rxjs/add/operator/map';

@Injectable()
export class GarageService implements IGarageService {

  appVersion = environment.appVersion;

  constructor(private accountService: AccountService, private http: Http) { }

  getGarageBalance(): Observable<any> {
    let headers = this.accountService.getTokenHeader();
    if (headers == null) return;
    let self = this;
    return new Observable(observer => {
      this.http.get(environment.url + `/api/v${this.appVersion}/Garage/Balance`, { headers: headers })
        .subscribe(x => {
          observer.next(x.json())
        });
    });
  }

  prepareUpgrade(): number {
    return 1000 * this.accountService.getGarage().GarageLevel;
  }

  public upgradeGarage(cost: number) {
    let headers = this.accountService.getTokenHeader();
    if (headers == null) return;
    headers.append("Content-Type", "application/x-www-form-urlencoded");
    let self = this;
    let content = `cost=${cost}`;
    this.http.post(environment.url + `/api/v${this.appVersion}/Garage/Upgrade`, content, { headers: headers })
      .subscribe(x => {
        console.log(x);
      });
  }
}
