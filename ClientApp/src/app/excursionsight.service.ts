import { environment } from '../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Tour } from './tour';
import { Client } from './client';
import { Sight } from './sight';
import { Excursion } from './excursion';

@Injectable()
export class ExcursionSightService {
  url: string
  constructor(private http: HttpClient) {
    this.url = environment.apiUrl;
  }

  insertSight(excursionId: number, sightId: number): Observable<any> {
    return this.http.post<any>(this.url + 'excursionsight/' + excursionId, sightId);
  }

  deleteSight(excursionId: number, sightId: number): Observable<any> {
    return this.http.delete<any>(this.url + 'excursionsight/' + excursionId + '/' + sightId);
  }
}
