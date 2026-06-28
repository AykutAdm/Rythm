import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SongService } from '../../../services/song-service';
import { ArtistService } from '../../../services/artist-service';
import { AlbumService } from '../../../services/album-service';
import { ResultSongDto } from '../../../models/song.model';
import { ResultArtistDto } from '../../../models/artist-model';
import { ResultAlbumDto } from '../../../models/album-model';
import { AudioService } from '../../../services/audio-service';
import { LikedSongsService } from '../../../services/liked-songs-service';
import { HistoryService } from '../../../services/history-service';
import { AuthService } from '../../../services/auth-service';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit {
  likedSongsService = inject(LikedSongsService);
  audioService = inject(AudioService);
  authService = inject(AuthService);

  private songService = inject(SongService);
  private artistService = inject(ArtistService);
  private albumService = inject(AlbumService);
  private historyService = inject(HistoryService);

  recommendations = signal<ResultSongDto[]>([]);

  songs = signal<ResultSongDto[]>([]);
  artists = signal<ResultArtistDto[]>([]);
  albums = signal<ResultAlbumDto[]>([]);

  ngOnInit() {
    this.songService.getAll().subscribe((data) => this.songs.set(data));
    this.artistService.getAll().subscribe((data) => this.artists.set(data));
    this.albumService.getAll().subscribe((data) => this.albums.set(data));
    this.loadRecommendations();
  }

  loadRecommendations() {
    const userId = this.authService.getUserId();
    if (userId <= 0) return;

    this.historyService.getRecommendations(userId).subscribe((data) => {
      this.recommendations.set(data);
    });
  }
}
