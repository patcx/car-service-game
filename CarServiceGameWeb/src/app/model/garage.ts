import { RepairProcess } from "./repair-process";
import { Worker } from "./worker";

export class Garage {
    private _garageId: string; // TODO: find better data type for guid
    private _cashBalance: number;
    private _garageLevel: number;
    private _employeedWorkers: Array<Worker>
    private _repairProcesses: Array<RepairProcess>


    constructor(garageId: string, cashBalance: number, garageLevel: number) {
        this._garageId = garageId;
        this._cashBalance = cashBalance;
        this._garageLevel = garageLevel;
        this._employeedWorkers = new Array();
        this._repairProcesses = new Array();
    }


    get GarageId(): string {
        return this._garageId;
    }

    set GarageId(id: string) {
        this._garageId = id;
    }

    get CashBalance(): number {
        return this._cashBalance;
    }

    set CashBalance(balance: number) {
        this._cashBalance = balance;
    }

    get GarageLevel(): number {
        return this._garageLevel;
    }

    set GarageLevel(level: number) {
        this._garageLevel = level;
    }

    get EmployeedWorkers(): Array<Worker> {
        return this._employeedWorkers;
    }

    set EmployeedWorkers(workers: Array<Worker>) {
        this._employeedWorkers = workers;
    }

    get RepairProcesses(): Array<RepairProcess> {
        return this._repairProcesses
    }

    set RepairProcesses(processes: Array<RepairProcess>) {
        this._repairProcesses = processes;
    }

    addWorker(worker: Worker) {
        this._employeedWorkers.splice(0, 0, worker);
    }

    removeWorker(worker: Worker) {
        this._employeedWorkers = this._employeedWorkers.filter(x => x.WorkerId != worker.WorkerId);
    }
}