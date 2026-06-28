import { inject, Injectable, signal } from '@angular/core';
import { ResultSongDto } from '../models/song.model';
import { AuthService } from './auth-service';
import { jwtDecode } from 'jwt-decode';
import { HistoryService } from './history-service';

@Injectable({
  providedIn: 'root',
})
export class AudioService {
  private authService = inject(AuthService);
  private historyService = inject(HistoryService);
  private audio = new Audio();

  currentSong = signal<any>(null);
  isPlaying = signal<boolean>(false);
  currentTime = signal<number>(0);
  duration = signal<number>(0);
  volume = signal<number>(1);
  showPremiumAlert = signal<boolean>(false);

  constructor() {
    this.audio.addEventListener('timeupdate', () => {
      this.currentTime.set(this.audio.currentTime);
    });

    this.audio.addEventListener('loadedmetadata', () => {
      this.duration.set(this.audio.duration);
    });

    this.audio.addEventListener('ended', () => {
      this.isPlaying.set(false);
      this.currentTime.set(0);
    });
  }

  isPremium(): boolean {
    const token = this.authService.getToken();
    if (!token) return false;
    try {
      const payload: any = jwtDecode(token);
      const roles = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || [];
      const rolesArray = Array.isArray(roles) ? roles : [roles];
      return rolesArray.includes('Premium');
    } catch {
      return false;
    }
  }

  play(song: any) {
    if (song.requiredPlan === 'Premium' && !this.isPremium()) {
      this.showPremiumAlert.set(true);
      return;
    }

    if (this.currentSong()?.songId === song.songId) {
      this.resume();
      return;
    }

    this.currentSong.set(song);
    this.audio.src = `https://localhost:7190/${song.audioUrl}`;
    this.audio.play();
    this.isPlaying.set(true);

    const userId = this.authService.getUserId();
    if (userId > 0) {
      this.historyService
        .recordListen({
          appUserId: userId,
          songId: song.songId,
        })
        .subscribe();
    }
  }

  resume() {
    this.audio.play();
    this.isPlaying.set(true);
  }

  pause() {
    this.audio.pause();
    this.isPlaying.set(false);
  }

  togglePlay() {
    if (this.isPlaying()) {
      this.pause();
    } else {
      this.resume();
    }
  }

  seek(time: number) {
    this.audio.currentTime = time;
    this.currentTime.set(time);
  }

  setVolume(volume: number) {
    this.audio.volume = volume;
    this.volume.set(volume);
  }

  formatTime(seconds: number): string {
    const mins = Math.floor(seconds / 60);
    const secs = Math.floor(seconds % 60);
    return `${mins}:${secs.toString().padStart(2, '0')}`;
  }
}
