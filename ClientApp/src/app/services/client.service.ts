import { environment } from '../../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Tour } from '../viewmodels/tour';
import { Client } from '../viewmodels/client';
import { Sight } from '../viewmodels/sight';
import { Excursion } from '../viewmodels/excursion';

@Injectable()
export class ClientService {
  url: string
  constructor(private http: HttpClient) {
    this.url = environment.apiUrl + 'clients/';
  }

  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(this.url);
  }

  getClient(id: string): Observable<Client> {
    return this.http.get<Client>(this.url + id);
  }

  insertClient(client: Client): Observable<Client> {
    return this.http.post<Client>(this.url, client);
  }
}
