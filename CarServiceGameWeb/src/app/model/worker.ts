export class Worker {
    private _workerId: string;
    private _name: string;
    private _efficiency: number;
    private _salary: number;


    constructor(id, name, efficiency, salary) {
        this.workerId = id;
        this.name = name;
        this.efficiency = efficiency;
        this.salary = salary;
    }

    public get workerId(): string {
        return this._workerId;
    }

    public set workerId(value: string) {
        this._workerId = value;
    }

    public get name(): string {
        return this._name;
    }

    public set name(value: string) {
        this._name = value;
    }

    public get efficiency(): number {
        return this._efficiency;
    }

    public set efficiency(value: number) {
        this._efficiency = value;
    }

    public get salary(): number {
        return this._salary;
    }

    public set salary(value: number) {
        this._salary = value;
    }

}