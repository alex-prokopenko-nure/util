import { environment } from '../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Tour } from './tour';
import { Client } from './client';
import { Sight } from './sight';
import { Excursion } from './excursion';

@Injectable()
export class SightService {
  url: string
  constructor(private http: HttpClient) {
    this.url = environment.apiUrl + 'sights/';
  }

  getAllSights(): Observable<Sight[]> {
    return this.http.get<Sight[]>(this.url);
  }

  getSights(id: string): Observable<Sight[]> {
    return this.http.get<Sight[]>(this.url + id);
  }

  insertSight(sight: Sight): Observable<Sight> {
    return this.http.post<Sight>(this.url, sight);
  }
}
