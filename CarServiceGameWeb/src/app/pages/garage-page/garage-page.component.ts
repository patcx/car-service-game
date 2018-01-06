import { Component, OnInit, Inject } from '@angular/core';
import { IRankingService } from '../../interfaces/ranking-service';
import { RepairProcess } from '../../model/repair-process';
import { AccountService } from '../../services/account.service';
import { AbstractPage } from '../abstract-page/abstract-page';

@Component({
  selector: 'app-garage-page',
  templateUrl: './garage-page.component.html',
  styleUrls: ['./garage-page.component.scss']
})
export class GaragePageComponent extends AbstractPage implements OnInit {

  stalls: Array<Stall>;
  numberOfStalls: number;

  constructor(private accountService: AccountService) {
    super();
   }

  ngOnInit() {
    this.setLoading(true);
    this.createStalls();
    this.setLoading(false);
  }

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

  getStalls(): Array<Stall> {
    return this.stalls;
  }
}

export class Stall {
  private _number: number;
  private _order: RepairProcess;

  constructor(number: number) {
    this.number = number;
  }


  public get number(): number {
    return this._number;
  }

  public set number(value: number) {
    this._number = value;
  }


  public get order(): RepairProcess {
    return this._order;
  }

  public set order(value: RepairProcess) {
    this._order = value;
  }
}
