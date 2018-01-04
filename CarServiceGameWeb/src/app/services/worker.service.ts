import { Http, Headers } from '@angular/http';
import { Injectable } from '@angular/core';
import { IWorkerService } from '../interfaces/worker-service';
import { Worker } from '../model/worker';
import { AccountService } from '../services/account.service';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable()
export class WorkerService implements IWorkerService {
    

    appVersion: string = environment.appVersion;
    workers: Array<Worker>;

    constructor(private http: Http, private accountService: AccountService) { }

    updateAvailableWorkers() {
        let headers = this.accountService.getTokenHeader();
        if (headers == null) return;
        let self = this;
        this.http.get(environment.url + `/api/v${this.appVersion}/Workers`, { headers: headers }).subscribe(x => self.createWorkersList(x.json()));
    }
    
    getWorkers(){
        return this.workers;
    }

    createWorkersList(response) {
        this.workers= new Array();
        let self = this;
        response.forEach(element => {
            self.workers.push(new Worker(element.WorkerId, element.Name, element.Efficiency, element.Salary));
        });
    }

    fireWorker(workerId: any) {
        let headers = this.accountService.getTokenHeader();
        if (headers == null) return;
        headers.append("Content-Type", "application/x-www-form-urlencoded");
        let self = this;
        let content = `workerId=${workerId}`;
        this.http.post(environment.url + `/api/v${this.appVersion}/Workers/Fire`, content, { headers: headers}).subscribe(x=>console.log(x));
    }

    employWorker(workerId: any) {
        let headers = this.accountService.getTokenHeader();
        if (headers == null) return;
        headers.append("Content-Type", "application/x-www-form-urlencoded");
        let self = this;
        let content = `workerId=${workerId}`;
        this.http.post(environment.url + `/api/v${this.appVersion}/Workers/Employ`, content, { headers: headers}).subscribe(x=>console.log(x));
    }
    upgradeWorker(workerId: any) {
        let cost = 100
        let headers = this.accountService.getTokenHeader();
        if (headers == null) return;
        headers.append("Content-Type", "application/x-www-form-urlencoded");
        let self = this;
        let content = `workerId=${workerId}&cost=${cost}`;
        this.http.post(environment.url + `/api/v${this.appVersion}/Workers/Upgrade`, content, { headers: headers}).subscribe(x=>console.log(x));
    }
}
