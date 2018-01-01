export class GarageRanking {
    garageName: string;
    balance: number;
    numberOfWorkers: number;
    numberOfCompletedOrders: number;
    efficiency: number;

    constructor(name,balance,noOfWorkers,noOfCompletedOrders,efficiency) {
        this.garageName = name;
        this.balance = balance;
        this.numberOfWorkers = noOfWorkers;
        this.numberOfCompletedOrders = noOfCompletedOrders;
        this.efficiency = efficiency;
    }
}