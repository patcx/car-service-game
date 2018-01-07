import { Component, OnInit, Inject } from '@angular/core';
import { IOrderService } from '../../interfaces/order-service';
import { Order } from '../../model/order';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';
import { AbstractPage } from '../abstract-page/abstract-page';
import { AccountService } from '../../services/account.service';
import { Stall } from '../../model/stall';
import { IStallService } from '../../interfaces/stall-service';
import { Worker } from "../../model/worker";
import { RepairProcess } from '../../model/repair-process';

@Component({
  selector: 'app-orders-page',
  templateUrl: './orders-page.component.html',
  styleUrls: ['./orders-page.component.css']
})
export class OrdersPageComponent extends AbstractPage implements OnInit {

  selectedOrder: Order;
  selectedWorker: Worker;

  constructor( @Inject('OrderService') private orderService: IOrderService, private _sanitizer: DomSanitizer,
    private accountService: AccountService, @Inject('StallService') private stallService: IStallService) {
    super();
  }

  ngOnInit() {
    this.setLoading(true);
    let self = this;
    this.orderService.updateOrders().subscribe(x => self.setLoading(false),
      error => {
        self.setLoading(false);
        alert("Error while loading orders");
      });
  }

  getOrders(): Order[] {
    return this.orderService.getOrders();
  }

  getWidth(width) {
    return this._sanitizer.bypassSecurityTrustStyle(width / 100 + '%');
  }

  accept(order: Order) {
    this.stallService.createStalls();
    this.selectedOrder = order;
  }

  notOrderSelected() {
    return (this.selectedOrder == null);
  }

  getWorkers() {
    let ret = this.accountService.getGarage().EmployeedWorkers;
    ret = ret.filter(x=> this.accountService.getGarage().RepairProcesses.every(y=> y.AssignedWorker.WorkerId != x.WorkerId))
    return ret;
  }

  disabledStall(stall: Stall) {
    return stall.order != null;
  }

  getWidthEfficiency(width) {
    return this._sanitizer.bypassSecurityTrustStyle(width + '%');
  }

  assignToStall(stall: Stall) {
    if (this.selectedOrder == null) return;
    if (this.selectedWorker == null) {
      if (this.getWorkers() == null || this.getWorkers().length == 0) {
        alert("You don't have any workers, hire new worker!")
        return
      }
      this.selectedWorker = this.getWorkers()[0];
    }
    let self = this;
    this.orderService.asignOrder(this.selectedOrder.RepairOrderId, this.selectedWorker.WorkerId, stall.number).subscribe(x => {
      if (x.status != 'ok') {
        alert("Error while assigning order");
      } else {
        alert("Order and Workers assigned to stall");
        let repairProcess = self.createNewRepairProcess(stall.number)
        self.accountService.getGarage().RepairProcesses.push(repairProcess);
        self.stallService.getStalls()[stall.number].order = repairProcess;
        self.reload();
      }
    }, error => {
      alert("Error while assigning order");
    });
  }

  reload() {
    this.selectedWorker = null;
    this.selectedOrder = null;
    this.setLoading(true);
    let self = this;
    this.orderService.updateOrders().subscribe(x => self.setLoading(false),
      error => {
        self.setLoading(false);
        alert("Error while loading orders");
      });
  }

  selectWorker(worker) {
    this.selectedWorker = worker;
  }

  createNewRepairProcess(stallNumber) {
    return new RepairProcess(this.selectedOrder, this.selectedWorker, stallNumber, new Date(Date.now()), false);
  }

  getStalls() {
    return this.stallService.getStalls();
  }

  isSelectedWorker(worker: Worker): boolean {
    if (this.selectedWorker == null) return false;
    return this.selectedWorker.WorkerId == worker.WorkerId;
  }
}
