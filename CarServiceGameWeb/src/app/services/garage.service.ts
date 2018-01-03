import { Http } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IGarageService } from '../interfaces/garage-service';
import { TokenService } from './token.service';

@Injectable()
export class GarageService implements IGarageService {

  appVersion = environment.appVersion;

  constructor(private tokenService: TokenService, private http: Http) { }

  getGarageBalance(): Observable<any> {
    let headers = this.tokenService.getTokenHeader();
    if (headers == null) return;
    let self = this;
    return new Observable(observer => {
      this.http.get(environment.url + `/api/v${this.appVersion}/Garage/Balance`, { headers: headers })
        .subscribe(x => observer.next(x.json()));
    });
  }

  public upgradeGarage(): Observable<any> {
    throw new Error('Not implemented yet.');
  }
}
