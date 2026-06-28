import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  private http = inject(HttpClient);

  baseUrl: string = 'https://localhost:7190/api/Roles/';

  assignRole(userId: number, role: string) {
    return this.http.post(this.baseUrl + 'assign-role', { userId, role });
  }

  removeRole(userId: number, role: string) {
    return this.http.post(this.baseUrl + 'remove-role', { userId, role });
  }
}
