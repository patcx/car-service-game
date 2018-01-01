import { Injectable } from '@angular/core';
import { IRankingService } from '../interfaces/ranking-service';

@Injectable()
export class RankingService implements IRankingService {
  
  constructor() { }

  getRanking() {
    throw new Error("Method not implemented.");
  }

}
