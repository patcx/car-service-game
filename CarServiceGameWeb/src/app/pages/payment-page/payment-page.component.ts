import { Component, OnInit, Inject } from '@angular/core';
import {IPaymentService} from '../../interfaces/payment-service'

@Component({
  selector: 'app-payment-page',
  templateUrl: './payment-page.component.html',
  styleUrls: ['./payment-page.component.css']
})
export class PaymentPageComponent implements OnInit {

  email : string;
  code : string;

  constructor(@Inject("PaymentService") private paymentService: IPaymentService ) { }

  ngOnInit() {
  }

  sendCode(){
    if(this.email == undefined || this.email.length < 1)
    {
      alert("Incorrect email");
      return;
    }

    this.paymentService.sendCode(this.email).subscribe(x=>{

      if(x.status == 'ok'){
      alert("Mail is sent");
          
      }
      else{
      alert("Cannot send email");        
      }

    }, error=>{
      alert("Cannot send email");
    });

  }

  processCode(){
    if(this.code == undefined || this.code.length < 1)
    {
      alert("Incorrect code");
      return;
    }

    this.paymentService.useCode(this.email).subscribe(x=>{

      if(x.status == 'ok'){
      alert("Your account balance is increased");
          
      }
      else{
      alert("Cannot use this code");        
      }

    }, error=>{
      alert("Cannot use this code");
    });

  }
}
