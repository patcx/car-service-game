import { Component, OnInit, Injectable, Inject } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { AccountService } from '../../services/account.service';
import { ILoginService } from '../../interfaces/login-service';
import { AbstractPage } from '../abstract-page/abstract-page';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent extends AbstractPage implements OnInit {

  name: string;
  password: string;

  constructor( @Inject('LoginService') private loginService: ILoginService, private accountService: AccountService) {
    super();
  }

  ngOnInit() {
  }

  isValidToken(): boolean {
    return this.accountService.isValidToken();
  }

  login(): void {
    let self = this;
    this.setLoading(true);
    this.loginService.login(this.name, this.password).subscribe((x) => {
      if (x.status != 'ok') {
        alert("Cannot login");
      }
      self.setLoading(false)
    },
      error => {
        self.setLoading(false);
        alert("Cannot login");
      });
  }

  createAccount(): void {
    let self = this;
    if (this.passwordIsCorrect(this.password)) {
      this.loginService.createAccount(this.name, this.password).subscribe((x) => {
        if (x.status != 'ok') {
          alert("Cannot create account");
        }
        self.setLoading(false)
      },
        error => {
          self.setLoading(false);
          alert("Cannot create account");
        });
    } else {
      alert('Password should contains: uppercase letter, lowercase letter and digit');
    }
  }

  passwordIsCorrect(password: string): boolean {
    let reg = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])");
    return reg.test(password);
  }
}
