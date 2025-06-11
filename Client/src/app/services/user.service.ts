import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { UserLogin } from '../models/user-login';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = 'https://localhost:7020/api/User';

  constructor(private http: HttpClient) {}

  login(user: UserLogin): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, user);
  }
}
