import { Injectable } from '@angular/core';
import { IRankingService } from '../interfaces/ranking-service';
import { Http, Headers } from '@angular/http';
import { AccountService } from './account.service';
import { environment } from '../../environments/environment';
import { GarageRanking } from '../model/garage-ranking';

import 'rxjs/add/operator/map';

@Injectable()
export class RankingService implements IRankingService {

  appVersion: string = environment.appVersion;
  garages: Array<GarageRanking>;

  constructor(private http: Http, private accountService: AccountService) {
    this.garages = new Array();
  }

  getRanking() {
    let headers = this.accountService.getTokenHeader();
    if (headers == null) return;
    let self = this;
    return this.http.get(environment.url + `/api/v${this.appVersion}/Garage/Ranking`, { headers: headers }).map(x => {
      self.createGaragesList(x.json())
    });
  }

  private createGaragesList(response) {
    this.garages = new Array();
    let self = this;
    response.forEach(element => {
      self.garages.push(new GarageRanking(element.Name, element.CashBalance, element.NumberOfWorkers,
        element.NumberOfCompletedOrders, element.Efficiency));
    });
  }

  getGaragesRanking(value: SortingValue): Array<GarageRanking> {
    switch (value) {
      case SortingValue.CASH:
        return this.garages.sort((a, b) => a.Balance < b.Balance ? 1 : -1);
      case SortingValue.WORKERS:
        return this.garages.sort((a, b) => a.NumberOfWorkers < b.NumberOfWorkers ? 1 : -1);
      case SortingValue.ORDERS:
        return this.garages.sort((a, b) => a.NumberOfCompletedOrders < b.NumberOfCompletedOrders ? 1 : -1);
      case SortingValue.EFFICIENCY:
        return this.garages.sort((a, b) => a.Efficiency < b.Efficiency ? 1 : -1);
      case SortingValue.NAME:
        return this.garages.sort((a, b) => a.GarageName > b.GarageName ? 1 : -1);
    }
  }

}

export enum SortingValue {
  CASH,
  WORKERS,
  ORDERS,
  EFFICIENCY,
  NAME,
}
