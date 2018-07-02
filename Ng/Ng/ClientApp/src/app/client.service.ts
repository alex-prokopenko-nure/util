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
  constructor(private http: HttpClient, @Inject('BASE_URL') url: string) {
    this.url = url;
  }

  getClients(): Observable<Client[]> {
    return this.http.get<Client[]>(this.url + 'api/Client/GetClients');
  }

  getClient(id: number): Observable<Client> {
    return this.http.get<Client>(this.url + 'api/Client/' + id);
  }

  insertClient(client: Client): Observable<Client> {
    return this.http.post<Client>(this.url + 'api/Client', client);
  }
}
