import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import jwt_decode from "jwt-decode";
import { Token } from '../../Core/Models/token';


@Injectable({
  providedIn: 'root'
})
export class TokenResolverService {
  public expiration: number = null;
  private authenticationChanged = new Subject<boolean>();
  constructor() { }

  public get IsExpired(): boolean {
    try {
      let x = jwt_decode(this.getToken());
      this.expiration = x.exp;
    }
    catch (ex) {
      this.expiration = null;
    }
    
    if (this.expiration) return (Date.now() >= this.expiration * 1000);
    return false;
  }
  public isAuthenticated(): boolean {
    return (!(window.localStorage['token'] === undefined ||
      window.localStorage['token'] === null ||
      window.localStorage['token'] === 'null' ||
      window.localStorage['token'] === 'undefined' ||
      window.localStorage['token'] === ''));
  }
  public isAuthenticationChanged(): Observable<boolean> {
    return this.authenticationChanged.asObservable();
  }
  public getToken(): any {
    if (window.localStorage['token'] === undefined ||
      window.localStorage['token'] === null ||
      window.localStorage['token'] === 'null' ||
      window.localStorage['token'] === 'undefined' ||
      window.localStorage['token'] === '') {
      return '';
    }
    let obj = JSON.parse(window.localStorage['token']);
    return obj.token;
  }
  public setToken(data: Token): void {
    this.setStorageToken(JSON.stringify(data));
    let x = jwt_decode(data.token);
    this.expiration = x.exp;
  }
  public failToken(): void {
    this.setStorageToken(undefined);
  }
  public logout(): void {
    this.setStorageToken(undefined);
    
  }
  private setStorageToken(value: any): void {
    window.localStorage['token'] = value;
    this.authenticationChanged.next(this.isAuthenticated());
  }
}
