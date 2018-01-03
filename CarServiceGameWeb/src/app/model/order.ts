export class Order {
    private _orderId: string;
    private _carName: string;
    private _requiredWork: number;
    private _reward: number;
    private _description: string;

    constructor(id, name, requiredWork, reward, desc) {
        this.orderId = id;
        this.carName = name;
        this.requiredWork = requiredWork;
        this.reward = reward;
        this.description = desc;
	}

	public get orderId(): string {
		return this._orderId;
	}

	public set orderId(value: string) {
		this._orderId = value;
	}

	public get carName(): string {
		return this._carName;
	}

	public set carName(value: string) {
		this._carName = value;
	}

	public get requiredWork(): number {
		return this._requiredWork;
	}

	public set requiredWork(value: number) {
		this._requiredWork = value;
	}

	public get reward(): number {
		return this._reward;
	}

	public set reward(value: number) {
		this._reward = value;
	}

	public get description(): string {
		return this._description;
	}

	public set description(value: string) {
		this._description = value;
	}


}