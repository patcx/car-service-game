import { Component, Inject, Input, OnInit } from '@angular/core';
import { RepairProcess } from '../../../model/repair-process';
import { Observable } from 'rxjs/Observable';
import { IOrderService } from '../../../interfaces/order-service';
import { AccountService } from '../../../services/account.service';

import 'rxjs/add/observable/timer';

@Component({
  selector: 'app-stall',
  templateUrl: './stall.component.html',
  styleUrls: ['./stall.component.scss']
})
export class StallComponent implements OnInit {

  @Input() stall: RepairProcess;

  timerValue: number;
  timerSubscription;

  constructor( @Inject('OrderService') private orderService: IOrderService, private accountService: AccountService) { }

  ngOnInit() {
    this.setTimeToEnd();
  }

  setTimeToEnd() {
    if (!this.isStallEmpty()) {
      let workTime = (this.stall.Order.RequiredWork / this.stall.AssignedWorker.Efficiency) * 1000
      this.timerValue = new Date(this.stall.CreatedDate).getTime() - Date.now() + workTime;
      let tmpTimer = Observable.timer(0, 1000);
      let self = this;
      this.timerSubscription = tmpTimer.subscribe(t => {
        self.timerValue -= 1000;
        if (self.timerValue < 0) {
          self.timerSubscription.unsubscribe();
        }
      });
    }
  }

  getStall() {
    if (this.stall) {
      return this.stall.CreatedDate
    }
  }

  isStallEmpty() {
    return (this.stall == null);
  }

  isOrderFinished() {
    return this.timerValue < 0;
  }

  completeOrder() {
    let self = this;
    let id = this.stall.Order.RepairOrderId;
    this.orderService.completeOrder(id).subscribe(x => self.orderRemoveFromGarage(id));
  }

  cancelOrder() {
    let self = this;
    let id = this.stall.Order.RepairOrderId;
    this.orderService.cancelOrder(id).subscribe(x => self.orderRemoveFromGarage(id));
  }

  immediatelyCompleteOrder() {
    let self = this;
    let id = this.stall.Order.RepairOrderId;
    this.orderService.immediatelyCompleteOrder(id).subscribe(x => self.orderRemoveFromGarage(id),
      error => console.log("Operation not allowed"));
  }

  orderRemoveFromGarage(id) {
    console.log(this.accountService.getGarage().RepairProcesses);
    this.stall = null;
    let processes = this.accountService.getGarage().RepairProcesses.filter(x => x.Order.RepairOrderId != id);
    this.accountService.getGarage().RepairProcesses = processes;
    console.log(this.accountService.getGarage().RepairProcesses);
  }
}
