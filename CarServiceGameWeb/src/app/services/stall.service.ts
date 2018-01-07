import { Injectable } from '@angular/core';
import { RepairProcess } from '../model/repair-process';
import { Stall } from '../model/stall';
import { AccountService } from './account.service';
import { IStallService } from '../interfaces/stall-service';

@Injectable()
export class StallService implements IStallService {

  stalls: Array<Stall>;
  numberOfStalls: number;

  constructor(private accountService: AccountService) { }

  createStalls() {
    if (this.stalls == null) {
      this.stalls = new Array();
      this.numberOfStalls = this.accountService.getGarage().GarageLevel;
      for (let i = 0; i < this.numberOfStalls; i++) {
        this.stalls.push(new Stall(i));
      }
      let self = this;
    }
    else {
      let tmpNumber = this.accountService.getGarage().GarageLevel;
      if (tmpNumber != this.numberOfStalls) {
        let newStalls: Array<Stall> = new Array();
        this.numberOfStalls = this.accountService.getGarage().GarageLevel;
        for (let i = 0; i < this.numberOfStalls; i++) {
          newStalls.push(this.stalls[i]);
        }
        this.stalls = newStalls;
      }
    }
    this.accountService.getGarage().RepairProcesses.forEach(x => this.stalls[x.StallNumber].order = x);
  }

  getStalls() {
    return this.stalls;
  }
}
