import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { ReplaySubject } from 'rxjs/ReplaySubject';

import { ApiService } from './api.service';
import { Asset } from '../models/index';
import { distinctUntilChanged, map } from 'rxjs/operators';
import { HttpParams } from '@angular/common/http';

@Injectable()
export class AssetService {

  public serviceURL = "/asset";
  
  constructor (private apiService: ApiService) {}

  getAll() : Observable<any> {
    return this.apiService.get(this.serviceURL);
  }

  get(id:string) : Observable<any> {
    return this.apiService.get(this.getUrl(id));
  }

  add(asset:Asset): Observable<Asset>{
    return this.apiService.post(this.serviceURL, asset);
  }

  update(asset:Asset){
    return this.apiService.put(this.serviceURL, asset);
  }

  delete(id:string){
    return this.apiService.delete(this.getUrl(id));
  }

  private getUrl(id){
    return this.serviceURL + "/" + id;
  }
}