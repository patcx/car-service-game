import { GarageRanking } from "../model/garage-ranking";

export interface IRankingService {
    
    getRanking();
    getGaragesRanking(value): Array<GarageRanking>;
}