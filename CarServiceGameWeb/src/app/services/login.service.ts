import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Rx';
import { environment } from '../../environments/environment'; 

@Injectable()
export class LoginService {

  token = null;

  constructor(private http: Http) { }

  public isValidToken() : boolean {
    return (this.token != null);
  }


  public login(name, password): void {
    let content = `name=${name}&password=${password}`;
    let headers = new Headers();
    console.log(content);
    headers.append("Content-Type","application/x-www-form-urlencoded");
    let self = this;
    this.http.post(environment.url + "/api/v1/Garage", content, {headers: headers}).subscribe(x => self.token = x.json().token);
   }
}
