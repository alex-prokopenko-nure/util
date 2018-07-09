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
    this.url = environment.apiUrl + 'excursionsights/';
  }

  insertSight(excursionId: string, sightId: string): Observable<any> {
	let ids: Pair = new Pair();
	ids.excursionId = excursionId;
	ids.sightId = sightId;
    return this.http.post<any>(this.url, ids);
  }

  deleteSight(excursionId: string, sightId: string): Observable<any> {
    return this.http.delete<any>(this.url + excursionId + '/' + sightId);
  }
}

export class Pair {
	excursionId: string;
	sightId: string;
}
