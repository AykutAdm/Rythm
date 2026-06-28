import { Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { SongService } from '../../../../services/song-service';
import { ArtistService } from '../../../../services/artist-service';
import { AlbumService } from '../../../../services/album-service';
import { GenreService } from '../../../../services/genre-service';
import { ResultArtistDto } from '../../../../models/artist-model';
import { ResultAlbumDto } from '../../../../models/album-model';
import { ResultGenreDto } from '../../../../models/genre-model';
import { CreateSongDto } from '../../../../models/song.model';
import { FileService } from '../../../../services/file-service';

@Component({
  selector: 'app-song-create',
  imports: [FormsModule, RouterLink],
  templateUrl: './song-create.html',
  styleUrl: './song-create.css',
})
export class SongCreate implements OnInit {
  private songService = inject(SongService);
  private artistService = inject(ArtistService);
  private albumService = inject(AlbumService);
  private genreService = inject(GenreService);
  private fileService = inject(FileService);
  private router = inject(Router);

  artists = signal<ResultArtistDto[]>([]);
  albums = signal<ResultAlbumDto[]>([]);
  genres = signal<ResultGenreDto[]>([]);

  isUploadingAudio = signal<boolean>(false);
  isUploadingImage = signal<boolean>(false);

  model: CreateSongDto = {
    title: '',
    coverImageUrl: '',
    audioUrl: '',
    durationInSeconds: 0,
    releaseDate: '',
    artistId: 0,
    albumId: 0,
    genreId: 0,
    requiredPlan: 'Free'
  };

  ngOnInit() {
    this.artistService.getAll().subscribe((data) => this.artists.set(data));
    this.albumService.getAll().subscribe((data) => this.albums.set(data));
    this.genreService.getAll().subscribe((data) => this.genres.set(data));
  }

  onAudioSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) return;

    const file = input.files[0];
    this.isUploadingAudio.set(true);

    this.fileService.uploadAudio(file).subscribe({
      next: (response) => {
        this.model.audioUrl = response.url;
        this.isUploadingAudio.set(false);
      },
      error: () => this.isUploadingAudio.set(false),
    });
  }

  onImageSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) return;

    const file = input.files[0];
    this.isUploadingImage.set(true);

    this.fileService.uploadImage(file).subscribe({
      next: (response) => {
        this.model.coverImageUrl = response.url;
        this.isUploadingImage.set(false);
      },
      error: () => this.isUploadingImage.set(false),
    });
  }

  onSubmit() {
    this.songService.create(this.model).subscribe({
      complete: () => {
        this.router.navigate(['/admin/songs']);
      },
      error: (err) => console.log(err),
    });
  }
}
