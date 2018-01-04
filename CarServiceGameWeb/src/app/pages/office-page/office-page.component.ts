import { Component, OnInit, Inject } from '@angular/core';
import { IGarageService } from '../../interfaces/garage-service';
import { IOrderService } from '../../interfaces/order-service';
import { RepairProcess } from '../../model/repair-process';
import { AbstractPage } from '../abstract-page/abstract-page';

@Component({
  selector: 'app-office-page',
  templateUrl: './office-page.component.html',
  styleUrls: ['./office-page.component.css']
})
export class OfficePageComponent extends AbstractPage implements OnInit {

  private balance: number;

  constructor( @Inject("GarageService") private garageService: IGarageService,
    @Inject('OrderService') private orderService: IOrderService) { 
      super();
    }

  ngOnInit() {
    let self = this;
    self.setLoading(true);
    let getBalanceComplete: boolean = false;
    let getHistoryComplete: boolean = false;
    this.garageService.getGarageBalance().subscribe(x => {
      self.balance = x.balance;
      getBalanceComplete = true;
      self.setLoading(!getBalanceComplete && !getHistoryComplete);
    });
    this.orderService.getHistoryOrdersFromAPI().subscribe(x => {
      getHistoryComplete = true;
      self.setLoading(!getBalanceComplete && !getHistoryComplete);
    });;
  }

  getBalance() {
    return this.balance;
  }

  upgradeGarage(event) {
    let cost = this.garageService.prepareUpgrade();
    if (cost > this.balance) {
      return;
    }
    this.garageService.upgradeGarage(cost);
  }

  getHistoryOrders(): Array<RepairProcess> {
    return this.orderService.getHistoryOrders();
  }
}
