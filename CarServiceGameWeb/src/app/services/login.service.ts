import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { environment } from '../../environments/environment';
import { ILoginService } from '../interfaces/login-service';
import { AccountService } from './account.service';
import { Garage } from '../model/garage';

@Injectable()
export class LoginService implements ILoginService {

  appVersion: string = environment.appVersion;
  garage: Garage;

  constructor(private http: Http, private accountService: AccountService) { }

  public login(name, password): Observable<any> {
    let content = `name=${name}&password=${password}`;
    let headers = new Headers();
    headers.append("Content-Type", "application/x-www-form-urlencoded");
    let self = this;
    return new Observable(observer => {
      this.http.post(environment.url + `/api/v${this.appVersion}/Garage`, content, { headers: headers }).subscribe(x => {
        self.accountService.setToken(x.json().token);
        let garage: Garage = x.json().garage;
        self.accountService.setGarage(garage);
        observer.next();
      })
    })
  };

  public createAccount(name, password): Observable<any> {
    let content = `name=${name}&password=${password}`;
    let headers = new Headers();
    headers.append("Content-Type", "application/x-www-form-urlencoded");
    let self = this;
    return new Observable(observer => {
      this.http.post(environment.url + `/api/v${this.appVersion}/Garage/Register`, content, { headers: headers }).subscribe(x => {
        self.accountService.setToken(x.json().token)
        self.accountService.setGarage(x.json().garage);
        observer.next();
      });
    });
  }
}
