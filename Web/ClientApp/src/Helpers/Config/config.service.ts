import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  private _config: { [key: string]: string };
  constructor() {
    this._config = {
      PathAPI: 'https://localhost:44315/api/'
    };
  }
  get setting(): { [key: string]: string } {
    return this._config;
  }
  get(key: any) {
    return this._config[key];
  }

 
}
