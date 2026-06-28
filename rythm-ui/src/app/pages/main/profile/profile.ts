import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { UserService } from '../../../services/user-service';
import { AuthService } from '../../../services/auth-service';
import { UpdateUserProfileDto, UserProfileDto } from '../../../models/user-model';
import { FormsModule } from '@angular/forms';
import { LikedSongsService } from '../../../services/liked-songs-service';
import { ResultSongDto } from '../../../models/song.model';

@Component({
  selector: 'app-profile',
  imports: [CommonModule, FormsModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {
  private userService = inject(UserService);
  private authService = inject(AuthService);

  likedSongsService = inject(LikedSongsService);
  likedSongs = signal<ResultSongDto[]>([]);

  profile = signal<UserProfileDto | null>(null);
  isEditing = signal<boolean>(false);

  model: UpdateUserProfileDto = {
    id: 0,
    firstName: '',
    lastName: '',
    profileImageUrl: '',
    birthDate: '',
  };

  ngOnInit() {
    this.load();
  }

  load() {
    const userId = this.authService.getUserId();
    this.userService.getProfile(userId).subscribe((data) => {
      this.profile.set(data);
    });
    this.userService.getLikedSongs(userId).subscribe((data) => {
      this.likedSongs.set(data);
    });
  }

  startEdit() {
    this.model = {
      id: this.profile()!.id,
      firstName: this.profile()!.firstName,
      lastName: this.profile()!.lastName,
      profileImageUrl: this.profile()!.profileImageUrl || '',
      birthDate: this.profile()!.birthDate.split('T')[0],
    };
    this.isEditing.set(true);
  }

  cancelEdit() {
    this.isEditing.set(false);
  }

  onSubmit() {
    this.userService.updateProfile(this.model).subscribe({
      complete: () => {
        this.load();
        this.isEditing.set(false);
      },
      error: () => {
        this.load();
        this.isEditing.set(false);
      },
    });
  }
}
