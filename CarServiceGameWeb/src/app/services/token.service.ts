import { Injectable } from '@angular/core';
import { Headers } from '@angular/http';

@Injectable()
export class TokenService {

  private token: string;

  constructor() { }

  isValidToken(): boolean {
    return this.token != null;
  }

  getToken(): string {
    return this.token;
  }

  setToken(token): void {
    this.token = token;
  }

  getTokenHeader(): Headers {
    let headers = new Headers();
    if (this.token == null) return null;
    headers.append("Authorization", "Bearer " + this.getToken());
    return headers;
  }
}
