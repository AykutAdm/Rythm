import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ResultUserDto, UpdateUserProfileDto, UserProfileDto } from '../models/user-model';
import { ResultSongDto } from '../models/song.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private http = inject(HttpClient);

  baseUrl: string = 'https://localhost:7190/api/Users/';

  getAll() {
    return this.http.get<ResultUserDto[]>(this.baseUrl);
  }

  getProfile(id: number) {
    return this.http.get<UserProfileDto>(this.baseUrl + id);
  }

  updateProfile(model: UpdateUserProfileDto) {
    return this.http.put(this.baseUrl, model);
  }


  likeSong(userId: number, songId: number) {
    return this.http.post(this.baseUrl + 'like-song', { appUserId: userId, songId });
  }

  unlikeSong(userId: number, songId: number) {
    return this.http.post(this.baseUrl + 'unlike-song', { appUserId: userId, songId });
  }

  getLikedSongs(userId: number) {
    return this.http.get<ResultSongDto[]>(this.baseUrl + 'liked-songs/' + userId);
  }
}
