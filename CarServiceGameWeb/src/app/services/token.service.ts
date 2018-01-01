import { Injectable } from '@angular/core';

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
}
