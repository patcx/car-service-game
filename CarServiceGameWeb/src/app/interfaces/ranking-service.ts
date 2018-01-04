import { GarageRanking } from "../model/garage-ranking";
import { Observable } from "rxjs";

export interface IRankingService {
    
    getRanking(): Observable<any>;
    getGaragesRanking(value): Array<GarageRanking>;
}