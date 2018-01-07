import { Component, OnInit, Inject } from '@angular/core';
import { IRankingService } from '../../interfaces/ranking-service';
import { RepairProcess } from '../../model/repair-process';
import { AccountService } from '../../services/account.service';
import { AbstractPage } from '../abstract-page/abstract-page';
import { IStallService } from '../../interfaces/stall-service';
import { Stall } from '../../model/stall';

@Component({
  selector: 'app-garage-page',
  templateUrl: './garage-page.component.html',
  styleUrls: ['./garage-page.component.scss']
})
export class GaragePageComponent extends AbstractPage implements OnInit {

  constructor(@Inject('StallService') private stallService: IStallService) {
    super();
   }

  ngOnInit() {
    this.setLoading(true);
    this.createStalls();
    this.setLoading(false);
  }

  createStalls() {
    this.stallService.createStalls();
  }

  getStalls(): Array<Stall> {
    return this.stallService.getStalls();
  }
}


