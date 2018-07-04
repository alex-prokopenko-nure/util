import { environment } from '../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Tour } from './tour';
import { Client } from './client';
import { Sight } from './sight';
import { Excursion } from './excursion';

@Injectable()
export class ExcursionService {
  url: string
  constructor(private http: HttpClient) {
    this.url = environment.apiUrl;
  }

  getExcursions(): Observable<Excursion[]> {
    return this.http.get<Excursion[]>(this.url + 'excursion');
  }

  getExcursion(id: string): Observable<Excursion> {
    return this.http.get<Excursion>(this.url + 'excursion/' + id);
  }

  insertExcursion(excursion: Excursion): Observable<Excursion> {
    return this.http.post<Excursion>(this.url + 'excursion', excursion);
  }

  updateExcursion(excursion: Excursion): Observable<Excursion> {
    return this.http.put<Excursion>(this.url + 'excursion/' + excursion.id, excursion);
  }

  deleteExcursion(id: string): Observable<string> {
    return this.http.delete<string>(this.url + 'excursion/' + id);
  }
}
