import { Component, OnInit, Injectable, Inject } from '@angular/core';
import { LoginService } from '../../services/login.service';
import { TokenService } from '../../services/token.service';
import { ILoginService } from '../../interfaces/login-service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  name: string;
  password: string;

  constructor(@Inject('LoginService') private loginService: ILoginService, private tokenService: TokenService) { }

  ngOnInit() {
  }

  isValidToken(): boolean {
    return this.tokenService.isValidToken();
  }

  login(): void {
    this.loginService.login(this.name, this.password);
  }
  createAccount(): void {
    if (this.passwordIsCorrect(this.password)) {
      this.loginService.createAccount(this.name, this.password);
    } else {
      alert('Password should contains: uppercase letter, lowercase letter and digit');
    }
  }

  passwordIsCorrect(password: string): boolean {
    return true;
    //TODO
  }
}
