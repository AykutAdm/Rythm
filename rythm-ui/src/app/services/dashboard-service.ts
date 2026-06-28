import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DashboardStats } from '../models/dashboard-model';

@Injectable({
  providedIn: 'root',
})
export class DashboardService {
  constructor(private http: HttpClient) {}

  baseUrl: string = 'https://localhost:7190/api/Dashboard/';

  getStats() {
    return this.http.get<DashboardStats>(this.baseUrl);
  }
}
