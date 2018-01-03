import { IOrderService } from '../interfaces/order-service';
import { Inject, Injectable } from '@angular/core';
import { IGarageService } from '../interfaces/garage-service';

@Injectable()
export class GarageViewModel {

    private garageBalance: number;

    constructor( @Inject("GarageService") private garageService: IGarageService,
        @Inject('OrderService') private orderService: IOrderService) {

    }

    getGarageBalance() {
        let self = this;
        this.garageService.getGarageBalance().subscribe(x => self.garageBalance = x.balance);
    }

    upgradeGarage() {
        this.garageService.upgradeGarage();
    }

    public get $garageBalance(): number {
        return this.garageBalance;
    }

    public set $garageBalance(value: number) {
        this.garageBalance = value;
    }

}