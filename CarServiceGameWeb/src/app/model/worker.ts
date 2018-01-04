export class Worker {
    private _workerId: string;
    private _name: string;
    private _efficiency: number;
    private _salary: number;


    constructor(id, name, efficiency, salary) {
        this.WorkerId = id;
        this.Name = name;
        this.Efficiency = efficiency;
        this.Salary = salary;
    }

    public get WorkerId(): string {
        return this._workerId;
    }

    public set WorkerId(value: string) {
        this._workerId = value;
    }

    public get Name(): string {
        return this._name;
    }

    public set Name(value: string) {
        this._name = value;
    }

    public get Efficiency(): number {
        return this._efficiency;
    }

    public set Efficiency(value: number) {
        this._efficiency = value;
    }

    public get Salary(): number {
        return this._salary;
    }

    public set Salary(value: number) {
        this._salary = value;
    }

}