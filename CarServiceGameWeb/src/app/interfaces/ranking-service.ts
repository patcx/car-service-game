import { GarageRanking } from "../model/garage-ranking";

export interface IRankingService {
    
    getRanking();
    createGaragesList(response);
    getGaragesRanking(value): Array<GarageRanking>;
}