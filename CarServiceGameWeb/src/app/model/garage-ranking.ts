export class GarageRanking {
    GarageName: string;
    Balance: number;
    NumberOfWorkers: number;
    NumberOfCompletedOrders: number;
    Efficiency: number;

    constructor(name, balance, noOfWorkers, noOfCompletedOrders, efficiency) {
        this.GarageName = name;
        this.Balance = balance;
        this.NumberOfWorkers = noOfWorkers;
        this.NumberOfCompletedOrders = noOfCompletedOrders;
        this.Efficiency = efficiency;
    }
}