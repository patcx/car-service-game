import { Injectable } from '@angular/core';
import { IRankingService } from '../interfaces/ranking-service';
import { Http, Headers } from '@angular/http';
import { AccountService } from './account.service';
import { environment } from '../../environments/environment';
import { GarageRanking } from '../model/garage-ranking';


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
    this.http.get(environment.url + `/api/v${this.appVersion}/Garage/Ranking`, { headers: headers }).subscribe(x => self.createGaragesList(x.json()));
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
    console.log(value);
    switch (value) {
      case SortingValue.CASH:
        return this.garages.sort((a, b) => a.balance < b.balance ? 1 : -1);
      case SortingValue.WORKERS:
        return this.garages.sort((a, b) => a.numberOfWorkers < b.numberOfWorkers ? 1 : -1);
      case SortingValue.ORDERS:
        return this.garages.sort((a, b) => a.numberOfCompletedOrders < b.numberOfCompletedOrders ? 1 : -1);
      case SortingValue.EFFICIENCY:
        return this.garages.sort((a, b) => a.efficiency < b.efficiency ? 1 : -1);
      case SortingValue.NAME:
        return this.garages.sort((a, b) => a.garageName > b.garageName ? 1 : -1);
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
