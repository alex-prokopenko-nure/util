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
  constructor(private http: HttpClient, @Inject('BASE_URL') url: string) {
    this.url = url;
  }

  getTours(): Observable<Tour[]> {
    return this.http.get<Tour[]>(this.url + 'api/Tour/GetTours');
  }

  insertTour(tour: Tour): Observable<Tour> {
    return this.http.post<Tour>(this.url + 'api/Tour', tour);
  }

  updateTour(tour: Tour): Observable<Tour> {
    return this.http.put<Tour>(this.url + 'api/Tour/' + tour.id, tour);
  }

  deleteTour(id: number): Observable<number> {
    return this.http.delete<number>(this.url + 'api/Tour/' + id);
  }
}
