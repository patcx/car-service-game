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
    

	public get Order(): Order {
		return this._order;
	}

	public set Order(value: Order) {
		this._order = value;
	}

	public get AssignedWorker(): Worker {
		return this._assignedWorker;
	}

	public set AssignedWorker(value: Worker) {
		this._assignedWorker = value;
	}

	public get StallNumber(): number {
		return this._stallNumber;
	}

	public set StallNumber(value: number) {
		this._stallNumber = value;
	}

	public get CreatedDate(): Date {
		return this._createdDate;
	}

	public set CreatedDate(value: Date) {
		this._createdDate = value;
	}

	public get IsCancelled(): boolean {
		return this._isCancelled;
	}

	public set IsCancelled(value: boolean) {
		this._isCancelled = value;
	}
    
}