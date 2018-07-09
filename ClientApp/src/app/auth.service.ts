import { environment } from '../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Tour } from './tour';
import { Client } from './client';
import { Sight } from './sight';
import { Excursion } from './excursion';
import { map, catchError } from 'rxjs/operators';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Credentials } from './credentials.interface';
import { AppUser, User } from './user';

@Injectable()
export class AuthService {
  url: string;
  constructor(private http: HttpClient) {
    this.url = environment.apiUrl;
  }

  register(email: string, firstName: string, lastName: string, password: string) {
    alert(email + password + firstName + lastName);
    return this.http.post<boolean>(this.url + 'accounts/register', { email, password, firstName, lastName });
  }

  login(email: string, password: string) {
    return this.http.post<User>(this.url + 'accounts/login', { email, password });
  }

  logout() {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('user_name');
  }

  public isLoggedIn() {
    return localStorage.getItem('auth_token') != null;
  }
}
