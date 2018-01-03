import { Order } from "./order";
import { Worker } from "./worker";

export class RepairProcess {
    private _order: Order;
    private _assignedWorker: Worker;
    private _stallNumber: number;
    private _createdDate: Date;
    private _isCancelled: boolean;


	constructor(order: Order, assignedWorker: Worker, stallNumber: number, createdDate: Date, isCancelled: boolean) {
		this._order = order;
		this._assignedWorker = assignedWorker;
		this._stallNumber = stallNumber;
		this._createdDate = createdDate;
		this._isCancelled = isCancelled;
	}
    

	public get order(): Order {
		return this._order;
	}

	public set order(value: Order) {
		this._order = value;
	}

	public get assignedWorker(): Worker {
		return this._assignedWorker;
	}

	public set assignedWorker(value: Worker) {
		this._assignedWorker = value;
	}

	public get stallNumber(): number {
		return this._stallNumber;
	}

	public set stallNumber(value: number) {
		this._stallNumber = value;
	}

	public get createdDate(): Date {
		return this._createdDate;
	}

	public set createdDate(value: Date) {
		this._createdDate = value;
	}

	public get isCancelled(): boolean {
		return this._isCancelled;
	}

	public set isCancelled(value: boolean) {
		this._isCancelled = value;
	}
    
}