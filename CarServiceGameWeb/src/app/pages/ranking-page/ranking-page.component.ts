import { Component, OnInit, Inject } from '@angular/core';
import { IRankingService } from '../../interfaces/ranking-service';
import { SortingValue } from '../../services/ranking.service';
import { FormsModule } from '@angular/forms';
import { GarageRanking } from '../../model/garage-ranking';
import { AbstractPage } from '../abstract-page/abstract-page';

@Component({
  selector: 'app-ranking-page',
  templateUrl: './ranking-page.component.html',
  styleUrls: ['./ranking-page.component.css'],
})

export class RankingPageComponent extends AbstractPage implements OnInit {

  sortingValues: Array<any>;
  selectedValue: SortingValue;

  constructor( @Inject('RankingService') private rankingService: IRankingService) {
    super();
  }

  ngOnInit() {
    let self = this;
    self.setLoading(true);
    this.rankingService.getRanking().subscribe(x => self.setLoading(false));
    this.createSortingValues();
  }

  getGaragesRanking() { // Wiem, nieładnie, jak starczy czasu przed kolokwiami, to naprawie :)
    if (this.selectedValue == 0) {
      return this.rankingService.getGaragesRanking(SortingValue.CASH);
    } else if (this.selectedValue == 1) {
      return this.rankingService.getGaragesRanking(SortingValue.WORKERS);
    } else if (this.selectedValue == 2) {
      return this.rankingService.getGaragesRanking(SortingValue.ORDERS);
    } else if (this.selectedValue == 3) {
      return this.rankingService.getGaragesRanking(SortingValue.EFFICIENCY);
    } else if (this.selectedValue == 4) {
      return this.rankingService.getGaragesRanking(SortingValue.NAME);
    }
  }

  createSortingValues() {
    this.selectedValue = SortingValue.ORDERS;
    this.sortingValues = new Array();
    this.sortingValues.push(SortingValue.CASH);
    this.sortingValues.push(SortingValue.WORKERS);
    this.sortingValues.push(SortingValue.ORDERS);
    this.sortingValues.push(SortingValue.EFFICIENCY);
    this.sortingValues.push(SortingValue.NAME);
  }

}
