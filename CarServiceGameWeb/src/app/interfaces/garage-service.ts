import { Observable } from "rxjs";

export interface IGarageService {

    upgradeGarage(cost);
    getGarageBalance(): Observable<any>;
    prepareUpgrade(): number;
}