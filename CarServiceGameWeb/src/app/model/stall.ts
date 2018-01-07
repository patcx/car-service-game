import { RepairProcess } from "./repair-process";

export class Stall {
    private _number: number;
    private _order: RepairProcess;
  
    constructor(number: number) {
      this.number = number;
    }
  
  
    public get number(): number {
      return this._number;
    }
  
    public set number(value: number) {
      this._number = value;
    }
  
  
    public get order(): RepairProcess {
      return this._order;
    }
  
    public set order(value: RepairProcess) {
      this._order = value;
    }
  }