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
    this.setLoading(true);
    let self = this;
    this.workerService.updateAvailableWorkers().subscribe(x=>self.setLoading(false), error=>{
      self.setLoading(false);
      alert("Error while loading workers");
    });
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

    this.workerService.fireWorker(worker.WorkerId).subscribe(x => {

      if (x.status == 'ok') {
        let garage = this.accountService.getGarage();
        var i = garage.EmployeedWorkers.findIndex(x => worker.WorkerId == x.WorkerId);
        garage.EmployeedWorkers.splice(i, 1);
        this.workerService.getWorkers().splice(0, 0, worker);
      }
      else {
        alert('Cannot fire the worker');
      }
    }, error => {
      alert('Cannot fire the worker');
    });


  }

  employ(worker: Worker) {
    this.workerService.employWorker(worker.WorkerId).subscribe(x => {

      if (x.status == 'ok') {
        let garage = this.accountService.getGarage();
        garage.EmployeedWorkers.splice(0, 0, worker);
        var i = this.workerService.getWorkers().findIndex(x => worker.WorkerId == x.WorkerId);
        this.workerService.getWorkers().splice(i, 1);
      }
      else {
        alert('Cannot employ the worker');

      }

    }, error => {
      alert('Cannot employ the worker');
    });
  }

  upgrade(worker: Worker) {

    if (worker.Efficiency * 50 > this.accountService.getGarage().CashBalance) {
      alert('Not enough money to upgrade the worker');
      return;
    }

    let self = this;
    this.workerService.upgradeWorker(worker).subscribe(x => {

      if (x.status == 'ok') {
        self.accountService.getGarage().CashBalance -= worker.Efficiency * 50;
        worker.Efficiency += 10;
      }
      else {
        alert('Cannot upgrade the worker');

      }

    }, error => {
      alert('Cannot upgrade the worker');
    });
  }

}
