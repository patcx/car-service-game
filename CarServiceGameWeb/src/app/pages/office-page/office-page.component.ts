import { Component, OnInit, Inject } from '@angular/core';
import { IGarageService } from '../../interfaces/garage-service';
import { IOrderService } from '../../interfaces/order-service';

@Component({
  selector: 'app-office-page',
  templateUrl: './office-page.component.html',
  styleUrls: ['./office-page.component.css']
})
export class OfficePageComponent implements OnInit {

  private balance: number;

  constructor( @Inject("GarageService") private garageService: IGarageService,
    @Inject('OrderService') private orderService: IOrderService) { }

  ngOnInit() {
    let self = this;
    this.garageService.getGarageBalance().subscribe(x => self.balance = x.balance);
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

}
