import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subscriber } from 'rxjs';
import { TokenResolverService } from '../../Helpers/Auth/token-resolver.service';
import { ConfigService } from '../../Helpers/Config/config.service';
import { Claim } from '../Models/claim';
import { Role } from '../Models/role';
import { Response } from '../Response/response';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class RoleService extends BaseService {
  private pathAPI = this.config.setting['PathAPI'];

  constructor(private http: HttpClient, private config: ConfigService, tokenResolverService: TokenResolverService) { super(tokenResolverService) }


  GetRoles(): Observable<Role[]> {
    let x: Observable<Role[]> = new Observable<Role[]>(observer => {

      return this.http.get<Response<Role>>(this.pathAPI + 'Role', super.header()).subscribe(resp => {
        super.processResponse(observer, resp);
      }, error => {
        observer.error(error);
        observer.complete();
      }); 

    });
    return x;
  }

  GetClaims(): Observable<Claim[]> {
    let x: Observable<Claim[]> = new Observable<Claim[]>(observer => {

      return this.http.get<Response<Claim>>(this.pathAPI + 'Claims', super.header()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }

  AddRole(role: Role): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.post<Response<any>>(this.pathAPI + 'Role', JSON.stringify(role), super.header()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
          observer.error(error);
          observer.complete();
      });

    });
    return x;
  }
  UpdateRole(role: Role): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.put<Response<any>>(this.pathAPI + 'Role', JSON.stringify(role), super.header()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }
  DeleteRole(role: Role): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      return this.http.delete(this.pathAPI + 'Role/' + role.Id, super.header()).subscribe(resp => {
        super.processResponse(observer, <Response<boolean>>resp);


      }, error => {
        observer.error(error);
        observer.complete();
      }); 

    });
    return x;
  }

  AddRoleClaim(roleId: number, claimId: number): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      return this.http.post(this.pathAPI + 'RoleClaim', JSON.stringify({ RoleId: roleId, ClaimId: claimId }), super.header()).subscribe(resp => {
        super.processResponse(observer, <Response<boolean>>resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }

  DeleteRoleClaim(roleId: number, claimId: number): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      return this.http.delete(this.pathAPI + 'RoleClaim/' + roleId + '/' + claimId, super.header()).subscribe(resp => {
        super.processResponse(observer, <Response<boolean>>resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }
}
