import { Component, inject, OnInit, signal, } from '@angular/core';
import { ResultArtistDto } from '../../../../models/artist-model';
import { ArtistService } from '../../../../services/artist-service';
import { RouterLink } from "@angular/router";


@Component({
  selector: 'app-artist-list',
  imports: [RouterLink],
  templateUrl: './artist-list.html',
  styleUrl: './artist-list.css',
})
export class ArtistList implements OnInit {

  private artistService = inject(ArtistService);

  artists = signal<ResultArtistDto[]>([]);

  ngOnInit() {
    this.loadArtists();
  }

  loadArtists() {
    this.artistService.getAll().subscribe(data => {
      this.artists.set(data);
    });
  }

  delete(id: number) {
     if (confirm('Silmek istediğinize emin misiniz?')) {
      this.artistService.delete(id).subscribe({
        next: () => {
          this.artists.update(list => list.filter(a => a.artistId !== id));
        }
      });
    }
  }

}
