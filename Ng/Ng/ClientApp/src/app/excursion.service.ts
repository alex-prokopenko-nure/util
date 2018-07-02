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
  constructor(private http: HttpClient, @Inject('BASE_URL') url: string) {
    this.url = url;
  }

  getExcursions(): Observable<Excursion[]> {
    return this.http.get<Excursion[]>(this.url + 'api/Excursion/GetExcursions');
  }

  getExcursion(id: number): Observable<Excursion> {
    return this.http.get<Excursion>(this.url + 'api/Excursion/' + id);
  }

  insertExcursion(excursion: Excursion): Observable<Excursion> {
    return this.http.post<Excursion>(this.url + 'api/Excursion', excursion);
  }

  updateExcursion(excursion: Excursion): Observable<Excursion> {
    return this.http.put<Excursion>(this.url + 'api/Excursion/' + excursion.id, excursion);
  }

  deleteExcursion(id: number): Observable<number> {
    return this.http.delete<number>(this.url + 'api/Excursion/' + id);
  }
}
