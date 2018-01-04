import { Observable } from "rxjs/Observable";
import { Worker } from "../model/Worker";

export interface IWorkerService {
    updateAvailableWorkers();
    getWorkers(): Array<Worker>;
    fireWorker(workerId);
    employWorker(workerId);
    upgradeWorker(workerId);
}