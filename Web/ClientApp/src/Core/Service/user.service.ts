import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenResolverService } from '../../Helpers/Auth/token-resolver.service';
import { ConfigService } from '../../Helpers/Config/config.service';
import { BaseService } from './base.service';
import { Response } from '../Response/response';
import { User } from '../Models/user';

class UserPassword {
  Username: string;
  Password: string;
}

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService  {
  private pathAPI = this.config.setting['PathAPI'];

  constructor(private http: HttpClient, private config: ConfigService, tokenResolverService: TokenResolverService) { super(tokenResolverService) }

  GetAllUsers(): Observable<User[]> {
    let x: Observable<User[]> = new Observable<User[]>(observer => {

      return this.http.get<Response<User>>(this.pathAPI + 'User', super.header()).subscribe(resp => {
        super.processResponse(observer, resp);
      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }

  UpdateRole(username: string, password: string): Observable<boolean> {
    let data = {
      Username: username,
      Password: password
    }

    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.put<Response<any>>(this.pathAPI + 'UserPassword', JSON.stringify(data), super.header()).subscribe(resp => {
        super.processResponse(observer, resp);
      }, error => {
        observer.error(error);
        observer.complete(); 
      });
       
    }); 
    return x; 
  }

  UploadPicture(formData: FormData): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.put<Response<any>>(this.pathAPI + 'UserPhoto', formData, super.headerImage()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });
    });
    return x;
  }

  UpdateUser(user: User): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.put<Response<any>>(this.pathAPI + 'User', JSON.stringify(user), super.header()).subscribe(resp => {
        super.processResponse(observer, resp);
      }, error => {
        observer.error(error);
        observer.complete();
      });
    });
    return x;
  }
  RegisterUser(user: User, password: string): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      let data = {
        user: user,
        password: password
      }
      this.http.put<Response<any>>(this.pathAPI + 'UserPublic', JSON.stringify(data) , super.header()).subscribe(resp => {
        super.processResponse(observer, resp);
      }, error => {
        observer.error(error);
        observer.complete();
      });
    });
    return x;
  }

  DeleteUser(userId: number): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      return this.http.delete(this.pathAPI + 'User/' + userId, super.header()).subscribe(resp => {
        super.processResponse(observer, <Response<boolean>>resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }
}
