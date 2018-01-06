import { Component, OnInit, Inject } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { Worker } from '../../model/worker';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';
import { IWorkerService } from '../../interfaces/worker-service';
import { WorkerService } from '../../services/worker.service';
import { AbstractPage } from '../abstract-page/abstract-page';
import { Garage } from '../../model/garage';


@Component({
  selector: 'app-workers-page',
  templateUrl: './workers-page.component.html',
  styleUrls: ['./workers-page.component.css']
})
export class WorkersPageComponent extends AbstractPage implements OnInit {

  constructor( @Inject("WorkerService") private workerService: IWorkerService, private accountService: AccountService, private _sanitizer: DomSanitizer) {
    super();
  }

  ngOnInit() {
    this.workerService.updateAvailableWorkers();
  }

  getAvailableWorkers(): Worker[] {
    return this.workerService.getWorkers();
  }

  getWorkersFromGarage(): Worker[] {
    let ret = this.accountService.getGarage().EmployeedWorkers;
    return ret;
  }

  getWidth(width) {
    return this._sanitizer.bypassSecurityTrustStyle(width + '%');
  }

  fire(worker: Worker) {
    this.workerService.fireWorker(worker.WorkerId);
    let garage: Garage = this.accountService.getGarage();
    garage.removeWorker(worker);
  }

  employ(worker: Worker) {
    this.workerService.employWorker(worker.WorkerId);
    let garage: Garage = this.accountService.getGarage();
    garage.addWorker(worker);
  }

  upgrade(worker: Worker) {
    this.workerService.upgradeWorker(worker.WorkerId);
  }

}
