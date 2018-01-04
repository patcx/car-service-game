import { Injectable } from '@angular/core';
import { Headers } from '@angular/http';
import { Garage } from '../model/garage';

@Injectable()
export class AccountService {

  private token: string;
  private garage: Garage;

  constructor() {
  }

  isValidToken(): boolean {
    return this.token != null;
  }

  getToken(): string {
    return this.token;
  }

  setToken(token): void {
    this.token = token;
  }

  getGarage(): Garage {
    return this.garage;
  }

  setGarage(garage: Garage): void {
    this.garage = garage;
  }

  getTokenHeader(): Headers {
    let headers = new Headers();
    if (this.token == null) return null;
    headers.append("Authorization", "Bearer " + this.getToken());
    return headers;
  }
}
