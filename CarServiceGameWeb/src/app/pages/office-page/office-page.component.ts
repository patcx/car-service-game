import { Component, OnInit, Inject } from '@angular/core';
import { IOfficeService } from '../../interfaces/office-service';
import { TokenService } from '../../services/token.service';

@Component({
  selector: 'app-office-page',
  templateUrl: './office-page.component.html',
  styleUrls: ['./office-page.component.css']
})
export class OfficePageComponent implements OnInit {

  constructor(@Inject('OfficeService') private officeService: IOfficeService) { }

  ngOnInit() {
    this.officeService.getGarageBalance();
    this.officeService.getHistoryOrders();
  }

  getBalance() {
    return this.officeService.getCashBalance();
  }

}
