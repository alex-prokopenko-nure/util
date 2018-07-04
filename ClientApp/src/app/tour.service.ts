import { environment } from '../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Tour } from './tour';
import { Client } from './client';
import { Sight } from './sight';
import { Excursion } from './excursion';

@Injectable()
export class TourService {
  url: string
  constructor(private http: HttpClient) {
    this.url = environment.apiUrl;
  }

  getTours(): Observable<Tour[]> {
    return this.http.get<Tour[]>(this.url + 'tour');
  }

  getTour(id: number): Observable<Tour> {
    return this.http.get<Tour>(this.url + 'tour/' + id);
  }

  insertTour(tour: Tour): Observable<Tour> {
    return this.http.post<Tour>(this.url + 'tour', tour);
  }

  updateTour(tour: Tour): Observable<Tour> {
    return this.http.put<Tour>(this.url + 'tour/' + tour.id, tour);
  }

  deleteTour(id: number): Observable<number> {
    return this.http.delete<number>(this.url + 'tour/' + id);
  }
}
