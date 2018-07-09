import { environment } from '../../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Tour } from '../viewmodels/tour';
import { Client } from '../viewmodels/client';
import { Sight } from '../viewmodels/sight';
import { Excursion } from '../viewmodels/excursion';

@Injectable()
export class ExcursionService {
  url: string
  constructor(private http: HttpClient) {
    this.url = environment.apiUrl + 'excursions/';
  }

  getExcursions(): Observable<Excursion[]> {
    return this.http.get<Excursion[]>(this.url);
  }

  getExcursion(id: string): Observable<Excursion> {
    return this.http.get<Excursion>(this.url + id);
  }

  insertExcursion(excursion: Excursion): Observable<Excursion> {
    return this.http.post<Excursion>(this.url, excursion);
  }

  updateExcursion(excursion: Excursion): Observable<Excursion> {
    return this.http.put<Excursion>(this.url + excursion.id, excursion);
  }

  deleteExcursion(id: string): Observable<string> {
    return this.http.delete<string>(this.url + id);
  }
}
