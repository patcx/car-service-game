import { Component, OnInit, Inject } from '@angular/core';
import { IRankingService } from '../../interfaces/ranking-service';
import { SortingValue } from '../../services/ranking.service';
import { FormsModule } from '@angular/forms';
import { GarageRanking } from '../../model/garage-ranking';

@Component({
  selector: 'app-ranking-page',
  templateUrl: './ranking-page.component.html',
  styleUrls: ['./ranking-page.component.css']
})
export class RankingPageComponent implements OnInit {

  constructor( @Inject('RankingService') private rankingService: IRankingService) { }

  ngOnInit() {
    this.rankingService.getRanking();
  }

  getGaragesRanking(): Array<GarageRanking> {
    return this.rankingService.getGaragesRanking(SortingValue.CASH);
  }

}
