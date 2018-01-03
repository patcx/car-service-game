import { Component, OnInit, Inject } from '@angular/core';
import { IOfficeService } from '../../interfaces/office-service';
import { TokenService } from '../../services/token.service';
import { GarageViewModel } from '../../view-model/garage-view-model';

@Component({
  selector: 'app-office-page',
  templateUrl: './office-page.component.html',
  styleUrls: ['./office-page.component.css']
})
export class OfficePageComponent implements OnInit {

  private balance: number;

  constructor(private garageViewModel: GarageViewModel, private orderViewModel) { }

  ngOnInit() {
    let self = this;
    this.garageViewModel.getGarageBalance();
  }

  getBalance() {
    return this.garageViewModel;
  }

  upgradeGarage() {
  }

}
