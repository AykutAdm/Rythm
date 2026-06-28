import { ChangeDetectorRef, Component, inject, OnInit, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { AlbumService } from '../../../../services/album-service';
import { ArtistService } from '../../../../services/artist-service';
import { ResultArtistDto } from '../../../../models/artist-model';
import { UpdateAlbumDto } from '../../../../models/album-model';

@Component({
  selector: 'app-album-update',
  imports: [FormsModule, RouterLink],
  templateUrl: './album-update.html',
  styleUrl: './album-update.css',
})
export class AlbumUpdate implements OnInit {

  private albumService = inject(AlbumService);
  private artistService = inject(ArtistService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private cdr = inject(ChangeDetectorRef);

  artists = signal<ResultArtistDto[]>([]);

  model: UpdateAlbumDto = {
    albumId: 0,
    title: '',
    coverImageUrl: '',
    releaseDate: ''
  }

  ngOnInit() {
    this.artistService.getAll().subscribe(data => {
      this.artists.set(data);
    });

    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.albumService.getById(id).subscribe({
      next: data => {
        this.model = {
          albumId: data.albumId,
          title: data.title,
          coverImageUrl: data.coverImageUrl,
          releaseDate: data.releaseDate
        };
        this.cdr.detectChanges();
      },
      error: err => console.log(err)
    });
  }

  onSubmit() {
    this.albumService.update(this.model).subscribe({
      complete: () => {
        this.router.navigate(['/admin/albums']);
      },
      error: err => console.log(err)
    });
  }
}
