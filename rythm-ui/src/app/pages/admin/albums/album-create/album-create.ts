import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AlbumService } from '../../../../services/album-service';
import { ArtistService } from '../../../../services/artist-service';
import { ResultArtistDto } from '../../../../models/artist-model';
import { CreateAlbumDto } from '../../../../models/album-model';

@Component({
  selector: 'app-album-create',
  imports: [FormsModule, RouterLink, CommonModule],
  templateUrl: './album-create.html',
  styleUrl: './album-create.css',
})
export class AlbumCreate {


  private albumService = inject(AlbumService);
  private artistService = inject(ArtistService);
  private router = inject(Router);

  artists = signal<ResultArtistDto[]>([]);

  model: CreateAlbumDto = {
    title: '',
    coverImageUrl: '',
    releaseDate: '',
    artistId: 0
  }


   ngOnInit() {
    this.artistService.getAll().subscribe(data => {
      this.artists.set(data);
    });
  }

  onSubmit() {
    this.albumService.create(this.model).subscribe({
      complete: () => {
        this.router.navigate(['/admin/albums']);
      },
      error: err => console.log(err)
    });
  }
}
