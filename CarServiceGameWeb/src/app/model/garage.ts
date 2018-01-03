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
    

    get garageId(): string {
        return this.garageId;
    }

    set garageId(id: string) {
        this._garageId = id;
    }

    get cashBalance(): number {
        return this._cashBalance;
    }

    set cashBalance(balance: number) {
        this._cashBalance = balance;
    }

    get garageLevel(): number {
        return this._garageLevel;
    }

    set garageLevel(level: number) {
        this._garageLevel = level;
    }

    get employeedWorkers(): Array<Worker> {
        return this._employeedWorkers;
    }

    set employeedWorkers(workers: Array<Worker>) {
        this._employeedWorkers = workers;
    }

    get repairProcesses(): Array<RepairProcess> {
        return this._repairProcesses
    }

    set repairProcesses(processes: Array<RepairProcess>){
        this._repairProcesses = processes;
    }
}