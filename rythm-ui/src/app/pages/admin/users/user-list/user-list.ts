import { Component, inject, OnInit, signal } from '@angular/core';
import { UserService } from '../../../../services/user-service';
import { RoleService } from '../../../../services/role-service';
import { CommonModule } from '@angular/common';
import { ResultUserDto } from '../../../../models/user-model';

@Component({
  selector: 'app-user-list',
  imports: [CommonModule],
  templateUrl: './user-list.html',
  styleUrl: './user-list.css',
})
export class UserList implements OnInit {


  private userService = inject(UserService);
  private roleService = inject(RoleService);

  users = signal<ResultUserDto[]>([]);
  openDropdownId = signal<number>(0);

  roles = ['User', 'Admin', 'Artist', 'Premium'];

  ngOnInit() {
    this.load();
  }

  load() {
    this.userService.getAll().subscribe(data => {
      this.users.set(data);
    });
  }

  toggleDropdown(userId: number) {
    if (this.openDropdownId() === userId) {
      this.openDropdownId.set(0);
    } else {
      this.openDropdownId.set(userId);
    }
  }

  assignRole(userId: number, role: string) {
    this.roleService.assignRole(userId, role).subscribe({
      next: () => {
        this.openDropdownId.set(0);
        this.load();
      },
    });
  }

  removeRole(userId: number, role: string) {
    this.roleService.removeRole(userId, role).subscribe({
      next: () => this.load(),
      error: () => this.load()
    });
  }
}
