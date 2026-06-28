import { ChangeDetectorRef, Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { SongService } from '../../../../services/song-service';
import { ArtistService } from '../../../../services/artist-service';
import { AlbumService } from '../../../../services/album-service';
import { GenreService } from '../../../../services/genre-service';
import { ResultArtistDto } from '../../../../models/artist-model';
import { ResultAlbumDto } from '../../../../models/album-model';
import { ResultGenreDto } from '../../../../models/genre-model';
import { UpdateSongDto } from '../../../../models/song.model';

@Component({
  selector: 'app-song-update',
  imports: [FormsModule, RouterLink],
  templateUrl: './song-update.html',
  styleUrl: './song-update.css',
})
export class SongUpdate implements OnInit {
  private songService = inject(SongService);
  private artistService = inject(ArtistService);
  private albumService = inject(AlbumService);
  private genreService = inject(GenreService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private cdr = inject(ChangeDetectorRef);

  artists = signal<ResultArtistDto[]>([]);
  albums = signal<ResultAlbumDto[]>([]);
  genres = signal<ResultGenreDto[]>([]);

  model: UpdateSongDto = {
    songId: 0,
    title: '',
    coverImageUrl: '',
    audioUrl: '',
    durationInSeconds: 0,
    playCount: 0,
    releaseDate: '',
    artistId: 0,
    albumId: 0,
    genreId: 0,
    requiredPlan: 'Free',
  };

  ngOnInit() {
    this.artistService.getAll().subscribe((data) => this.artists.set(data));
    this.albumService.getAll().subscribe((data) => this.albums.set(data));
    this.genreService.getAll().subscribe((data) => this.genres.set(data));

    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.songService.getById(id).subscribe({
      next: (data) => {
        this.model = {
          songId: data.songId,
          title: data.title,
          coverImageUrl: data.coverImageUrl,
          audioUrl: data.audioUrl,
          durationInSeconds: data.durationInSeconds,
          playCount: data.playCount,
          releaseDate: data.releaseDate.split('T')[0],
          artistId: data.artistId,
          albumId: data.albumId,
          genreId: data.genreId,
          requiredPlan: data.requiredPlan
        };
        this.cdr.detectChanges();
      },
      error: (err) => console.log(err),
    });
  }

  onSubmit() {
    this.songService.update(this.model).subscribe({
      complete: () => {
        this.router.navigate(['/admin/songs']);
      },
      error: (err) => console.log(err),
    });
  }
}
