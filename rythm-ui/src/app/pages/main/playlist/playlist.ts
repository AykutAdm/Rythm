import { CommonModule } from '@angular/common';
import { Component, computed, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PlaylistService } from '../../../services/playlist-service';
import { AuthService } from '../../../services/auth-service';
import { SongService } from '../../../services/song-service';
import { AudioService } from '../../../services/audio-service';
import { HistoryService } from '../../../services/history-service';
import { ActivatedRoute, Router } from '@angular/router';
import { GetPlaylistByIdDto } from '../../../models/playlist-model';
import { ResultSongDto } from '../../../models/song.model';

@Component({
  selector: 'app-playlist',
  imports: [CommonModule, FormsModule],
  templateUrl: './playlist.html',
  styleUrl: './playlist.css',
})
export class Playlist implements OnInit {
  private playlistService = inject(PlaylistService);
  authService = inject(AuthService);
  private songService = inject(SongService);
  private historyService = inject(HistoryService);
  audioService = inject(AudioService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);

  playlist = signal<GetPlaylistByIdDto | null>(null);
  allSongs = signal<ResultSongDto[]>([]);
  recommendations = signal<ResultSongDto[]>([]);
  isAddSongOpen = signal<boolean>(false);

  playlistRecommendations = computed(() => {
    const playlistSongIds = new Set(this.playlist()?.songs.map((s) => s.songId) ?? []);
    return this.recommendations().filter((s) => !playlistSongIds.has(s.songId));
  });

  ngOnInit() {
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.isAddSongOpen.set(false);
      this.loadPlaylist(id);
    });

    this.songService.getAll().subscribe((data) => this.allSongs.set(data));
    this.loadRecommendations();
  }

  loadPlaylist(id: number) {
    this.playlistService.getById(id).subscribe({
      next: (data) => this.playlist.set(data),
      error: (err) => console.log(err),
    });
  }

  loadRecommendations() {
    const userId = this.authService.getUserId();
    if (userId <= 0) return;

    this.historyService.getRecommendations(userId).subscribe((data) => {
      this.recommendations.set(data);
    });
  }

  addSong(songId: number) {
    const id = this.playlist()!.playlistId;
    this.playlistService.addSong(id, songId).subscribe({
      next: () => this.loadPlaylist(id),
      error: (err) => console.log(err),
    });
  }

  removeSong(songId: number) {
    const id = this.playlist()!.playlistId;
    this.playlistService.removeSong(id, songId).subscribe({
      next: () => this.loadPlaylist(id),
      error: (err) => console.log(err),
    });
  }

  deletePlaylist() {
    const playlist = this.playlist();
    if (!playlist) return;
    const onay = confirm(`"${playlist.name}" playlist'i silinsin mi?`);
    if (!onay) return;
    this.playlistService.delete(playlist.playlistId).subscribe({
      next: () => this.router.navigate(['/home']),
      error: (err) => console.log(err),
    });
  }
}
