import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest, LoginResponse, RegisterRequest } from '../models/auth.model';
import { jwtDecode } from 'jwt-decode';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}

  baseUrl: string = 'https://localhost:7190/api/Auth/';

  register(model: RegisterRequest) {
    return this.http.post(this.baseUrl + 'register', model);
  }

  login(model: LoginRequest) {
    return this.http.post<LoginResponse>(this.baseUrl + 'login', model);
  }

  logout() {
    localStorage.removeItem('token');
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  isLoggedIn() {
    return this.getToken() !== null;
  }

  getUserId(): number {
    const token = this.getToken();
    if (!token) return 0;
    try {
      const payload: any = jwtDecode(token);
      return Number(
        payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'],
      );
    } catch {
      return 0;
    }
  }

  // getUserId(): number {
  // const token = this.getToken();
  // if (!token) return 0;
  // const payload = JSON.parse(atob(token.split('.')[1]));
  // return Number(payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']);
  // }
}
