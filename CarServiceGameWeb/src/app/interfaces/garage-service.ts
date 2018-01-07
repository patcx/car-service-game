import { Observable } from "rxjs";

export interface IGarageService {

    upgradeGarage(cost): Observable<any>;
    getGarageBalance(): Observable<any>;
    prepareUpgrade(): number;
}