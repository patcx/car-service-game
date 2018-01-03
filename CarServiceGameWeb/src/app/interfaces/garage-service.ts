import { Observable } from "rxjs";

export interface IGarageService {
    
    upgradeGarage(): Observable<any>;
    getGarageBalance(): Observable<any>;
}