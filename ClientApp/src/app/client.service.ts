import { environment } from '../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Tour } from './tour';
import { Client } from './client';
import { Sight } from './sight';
import { Excursion } from './excursion';

@Injectable()
export class ClientService {
  url: string
  constructor(private http: HttpClient) {
    this.url = environment.apiUrl;
  }

  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(this.url + 'client');
  }

  getClient(id: string): Observable<Client> {
    return this.http.get<Client>(this.url + 'client/' + id);
  }

  insertClient(client: Client): Observable<Client> {
    return this.http.post<Client>(this.url + 'client', client);
  }
}
