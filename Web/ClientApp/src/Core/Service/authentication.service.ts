import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenResolverService } from '../../Helpers/Auth/token-resolver.service';
import { ConfigService } from '../../Helpers/Config/config.service';
import { Login } from '../Models/login';
import { Token } from '../Models/token';
import { User } from '../Models/user';
import { UserPhoto } from '../Models/user-photo';
import { SingleResponse } from '../Response/single-response';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService extends BaseService {
  user: User = null;
  expirationDate: number = 0;
  private pathAPI = this.config.setting['PathAPI'];
  constructor(private http: HttpClient, private config: ConfigService, private tokenResolverService: TokenResolverService) { super(tokenResolverService) }


  Authorize(loginInfo: Login): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      return this.http.post<SingleResponse<Token>>(this.pathAPI + 'Authentication', JSON.stringify(loginInfo), super.header()).subscribe(resp => {
        if (resp.data && resp.isSuccess) {
          this.tokenResolverService.setToken(resp.data);
          this.expirationDate = this.tokenResolverService.expiration;
          observer.next(true);
        } else {
          observer.error(resp.message);
        }
        observer.complete();
      }, error => {
          observer.error(error);
          observer.complete();
      });

    });
    return x; 
  }


  Logout(): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.tokenResolverService.logout();
      observer.next(true);
      observer.complete();
    })
    return x;
  }

  GetUserInfo(): Observable<User> {
    let x: Observable<User> = new Observable<User>(observer => {
      if (this.user) {
        observer.next(this.user);
      } else {

        return this.http.get<SingleResponse<User>>(this.pathAPI + 'Authentication', super.header()).subscribe(resp => {
          this.user = resp.data;
          observer.next(resp.data);
          observer.complete();
        }, error => {
            observer.error(error);
            observer.complete();
        }); 
      }
    });
    return x;
  } 

  IsAuthenticated(): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      let isAuth = this.tokenResolverService.isAuthenticated();
      if (isAuth) {
        observer.next(true);
      } 
      this.tokenResolverService.isAuthenticationChanged().subscribe(resp => {
        observer.next(resp);
      });
    });
    return x;
  } 


  GetUserPhoto(id: number = -1): Observable<UserPhoto> {
    let x: Observable<UserPhoto> = new Observable<UserPhoto>(observer => {
      return this.http.get<SingleResponse<UserPhoto>>(this.pathAPI + 'UserPhoto' + '/' + id, super.header()).subscribe(resp => {
        observer.next(resp.data);
        observer.complete();
      }, error => {
        observer.error(error);
        observer.complete();
      });  
      
    });
    return x;
  } 
}
