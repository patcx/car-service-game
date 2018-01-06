export class Order {
    private _orderId: string;
    private _carName: string;
    private _requiredWork: number;
    private _reward: number;
    private _description: string;

    constructor(id, name, requiredWork, reward, desc) {
        this.RepairOrderId = id;
        this.CarName = name;
        this.RequiredWork = requiredWork;
        this.Reward = reward;
        this.Description = desc;
	}

	public get RepairOrderId(): string {
		return this._orderId;
	}

	public set RepairOrderId(value: string) {
		this._orderId = value;
	}

	public get CarName(): string {
		return this._carName;
	}

	public set CarName(value: string) {
		this._carName = value;
	}

	public get RequiredWork(): number {
		return this._requiredWork;
	}

	public set RequiredWork(value: number) {
		this._requiredWork = value;
	}

	public get Reward(): number {
		return this._reward;
	}

	public set Reward(value: number) {
		this._reward = value;
	}

	public get Description(): string {
		return this._description;
	}

	public set Description(value: string) {
		this._description = value;
	}


}