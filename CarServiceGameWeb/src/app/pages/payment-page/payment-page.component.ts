import { Component, OnInit, Inject } from '@angular/core';
import { IPaymentService } from '../../interfaces/payment-service'
import { IGarageService } from '../../interfaces/garage-service'
import { AbstractPage } from '../abstract-page/abstract-page';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-payment-page',
  templateUrl: './payment-page.component.html',
  styleUrls: ['./payment-page.component.css']
})
export class PaymentPageComponent extends AbstractPage implements OnInit {

  email: string;
  code: string;

  constructor( @Inject("PaymentService") private paymentService: IPaymentService,
    @Inject("GarageService") private garageService: IGarageService,
    private accountService: AccountService) {
    super();
  }

  ngOnInit() {
  }

  sendCode() {
    if (this.email == undefined || this.email.length < 1) {
      alert("Incorrect email");
      return;
    }

    this.setLoading(true);
    let self = this;
    this.paymentService.sendCode(this.email).subscribe(x => {
      this.setLoading(false);
      if (x.status == 'ok') {
        alert("Mail is sent");

      }
      else {
        alert("Cannot send email");
      }

    }, error => {
      this.setLoading(false);
      alert("Cannot send email");
    });

  }

  processCode() {
    if (this.code == undefined || this.code.length < 1) {
      alert("Incorrect code");
      return;
    }
    this.setLoading(true);
    let self = this;
    this.paymentService.useCode(this.code).subscribe(x => {

      if (x.status == 'ok') {

        self.garageService.getGarageBalance().map(x => x.balance).subscribe(b => {
          self.accountService.getGarage().CashBalance = b;
          self.setLoading(false);
          alert("Your account balance is increased");
        }, error => {
          self.setLoading(false);
          alert("Your account balance is increased");
        })


      }
      else {
        self.setLoading(false);

        alert("Cannot use this code");
      }

    }, error => {
      self.setLoading(false);
      alert("Cannot use this code");
    });

  }
}
