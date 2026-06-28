import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth-service';
import { jwtDecode } from 'jwt-decode';

export const adminGuard: CanActivateFn = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (!authService.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }

  const token = authService.getToken();
  if (!token) {
    router.navigate(['/login']);
    return false;
  }

  // const payload = JSON.parse(atob(token.split('.')[1]));
  // const roles: string[] = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || [];
  // const rolesArray = Array.isArray(roles) ? roles : [roles];

  const payload: any = jwtDecode(token);
  const roles = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || [];
  const rolesArray = Array.isArray(roles) ? roles : [roles];

  if (rolesArray.includes('Admin')) {
    return true;
  }

  router.navigate(['/403']);
  return false;
};
