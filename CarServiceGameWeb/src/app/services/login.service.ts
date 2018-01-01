import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { environment } from '../../environments/environment'; 
import { ILoginService } from '../interfaces/login-service';
import { TokenService } from './token.service';

@Injectable()
export class LoginService implements ILoginService{

  appVersion: string = environment.appVersion;

  constructor(private http: Http, private tokenService: TokenService) { }

  public login(name, password): void {
    let content = `name=${name}&password=${password}`;
    let headers = new Headers();
    console.log(content);
    headers.append("Content-Type","application/x-www-form-urlencoded");
    let self = this;
    this.http.post(environment.url + `/api/v${this.appVersion}/Garage`, content, {headers: headers}).subscribe(x => self.tokenService.setToken(x.json().token));
   }

   public createAccount(name, password): void {
    let content = `name=${name}&password=${password}`;
    let headers = new Headers();
    console.log(content);
    headers.append("Content-Type","application/x-www-form-urlencoded");
    let self = this;
    this.http.post(environment.url + `/api/v${this.appVersion}/Garage/Register`, content, {headers: headers}).subscribe(x => self.tokenService.setToken(x.json().token));
   }
}
