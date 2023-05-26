import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TokenResolverService } from '../../Helpers/Auth/token-resolver.service';
import { ConfigService } from '../../Helpers/Config/config.service';
import { Role } from '../Models/role';
import { BaseService } from './base.service';
import { Response } from '../Response/response';
import { Medicine } from '../Models/medicine';
import { Pharmacy, PharmacyMedicine } from '../Models/pharmacy';

@Injectable({
  providedIn: 'root'
})
export class PharmacyService extends BaseService{
  private pathAPI = this.config.setting['PathAPI'];

  constructor(private http: HttpClient, private config: ConfigService, tokenResolverService: TokenResolverService) { super(tokenResolverService) }


  GetMedicine(): Observable<Medicine[]> {
    let x: Observable<Medicine[]> = new Observable<Medicine[]>(observer => {

      return this.http.get<Response<Medicine>>(this.pathAPI + 'Medicine', super.header()).subscribe(resp => {
        super.processResponse(observer, resp);
      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }


  AddMedicine(data: Medicine): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.post<Response<any>>(this.pathAPI + 'Medicine', JSON.stringify(data), super.header()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }
  UpdateMedicine(data: FormData): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.put<Response<any>>(this.pathAPI + 'Medicine', data, super.headerImage()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }
  DeleteMedicine(data: Medicine): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      return this.http.delete(this.pathAPI + 'Medicine/' + data.Id, super.header()).subscribe(resp => {
        super.processResponse(observer, <Response<boolean>>resp);


      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }



  GetPharmacies(): Observable<Pharmacy[]> {
    let x: Observable<Pharmacy[]> = new Observable<Pharmacy[]>(observer => {

      return this.http.get<Response<Pharmacy>>(this.pathAPI + 'Pharmacy', super.header()).subscribe(resp => {
        super.processResponse(observer, resp);
      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }


  AddPharmacy(data: Pharmacy): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.post<Response<any>>(this.pathAPI + 'Pharmacy', JSON.stringify(data), super.header()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }
  UpdatePharmacy(data: Pharmacy): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.put<Response<any>>(this.pathAPI + 'Pharmacy', JSON.stringify(data), super.header()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }
  DeletePharmacy(data: Pharmacy): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      return this.http.delete(this.pathAPI + 'Pharmacy/' + data.Id, super.header()).subscribe(resp => {
        super.processResponse(observer, <Response<boolean>>resp);


      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }

  Upload(formData: FormData): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.post<Response<any>>(this.pathAPI + 'PharmacyMedicine', formData, super.headerImage()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });
    });
    return x;
  }
  UpdatePharmacyMedicine(data: PharmacyMedicine): Observable<boolean> {
    let x: Observable<boolean> = new Observable<boolean>(observer => {
      this.http.put<Response<any>>(this.pathAPI + 'PharmacyMedicine', JSON.stringify(data), super.header()).subscribe(resp => {
        super.processResponse(observer, resp);

      }, error => {
        observer.error(error);
        observer.complete();
      });

    });
    return x;
  }
}
