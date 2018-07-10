import { environment } from '../../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Tour } from '../viewmodels/tour';
import { Client } from '../viewmodels/client';
import { Sight } from '../viewmodels/sight';
import { Excursion } from '../viewmodels/excursion';

@Injectable()
export class TourService {
  url: string;
  mode: string;
  constructor(private http: HttpClient) {
    this.url = environment.apiUrl + 'tours/';
  }

  getTours(): Observable<Tour[]> {
    return this.http.get<Tour[]>(this.url);
  }

  getTour(id: string): Observable<Tour> {
    return this.http.get<Tour>(this.url + id);
  }

  insertTour(tour: Tour): Observable<Tour> {
    return this.http.post<Tour>(this.url, tour);
  }

  updateTour(tour: Tour): Observable<Tour> {
    return this.http.put<Tour>(this.url + tour.id, tour);
  }

  deleteTour(id: string): Observable<string> {
    return this.http.delete<string>(this.url + id);
  }
}
