import { Component, OnInit, Inject } from '@angular/core';
import { AccountService } from '../../services/account.service';
import { Worker } from '../../model/worker';
import { DomSanitizer, SafeResourceUrl, SafeUrl } from '@angular/platform-browser';
import { IWorkerService } from '../../interfaces/worker-service';
import { WorkerService } from '../../services/worker.service';
import { AbstractPage } from '../abstract-page/abstract-page';


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
    return this.accountService.getGarage().EmployeedWorkers;
  }

  getWidth(width) {
    return this._sanitizer.bypassSecurityTrustStyle(width + '%');
  }

  fire(worker:Worker) {
    this.workerService.fireWorker(worker.WorkerId);
    this.accountService.getGarage().removeWorker(worker);
  }

  employ(worker:Worker) {
    this.workerService.employWorker(worker.WorkerId);
    this.accountService.getGarage().addWorker(worker);    
  }

  upgrade(worker:Worker) {
    this.workerService.upgradeWorker(worker.WorkerId);
  }

}
