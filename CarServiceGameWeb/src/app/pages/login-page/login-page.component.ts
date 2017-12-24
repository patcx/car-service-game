import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {

  name: string;
  password: string;

  constructor(private loginService: LoginService) { }

  ngOnInit() {
  }

  isValidToken() {
    return this.loginService.isValidToken();
  }

  login() {
    this.loginService.login(this.name, this.password);
  }
}
