import { environment } from '../../environments/environment';
import { Injectable, Inject } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Tour } from '../viewmodels/tour';
import { Client } from '../viewmodels/client';
import { Sight } from '../viewmodels/sight';
import { Excursion } from '../viewmodels/excursion';
import { map, catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { AppUser, User } from '../viewmodels/user';

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
