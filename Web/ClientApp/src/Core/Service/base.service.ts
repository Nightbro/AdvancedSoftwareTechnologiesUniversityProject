import { HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subscriber } from 'rxjs';
import { TokenResolverService } from '../../Helpers/Auth/token-resolver.service';
import { Response } from '../Response/response';

@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor(private authService: TokenResolverService) { }


  public processResponse(observer: Subscriber<any>, response: Response<any>) {
    if (response.isSuccess) {
      observer.next(response.data);
    } else {
      observer.error(response.message);
    }
    observer.complete()
  }

  public header() {
    let header = new HttpHeaders({ 'Content-Type': 'application/json' });
    if (this.authService.IsExpired) {
       
      this.authService.logout(); 
      window.location.reload();  
    }
    if (this.authService.isAuthenticated()) {
      header = header.append('Authorization', 'Bearer ' + this.authService.getToken());
    }
    return { headers: header };
  }

  public headerImage() {

    let header = new HttpHeaders({  });
    if (this.authService.IsExpired) {

      this.authService.logout();
      window.location.reload();
    }
    if (this.authService.isAuthenticated()) {
      header = header.append('Authorization', 'Bearer ' + this.authService.getToken());
    }
    return { headers: header };
  }
  public setToken(data: any) {
    this.authService.setToken(data);
  }

}
