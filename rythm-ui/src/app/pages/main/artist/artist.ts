import { Component, inject, OnInit, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ArtistService } from '../../../services/artist-service';
import { GetArtistByIdDto } from '../../../models/artist-model';

@Component({
  selector: 'app-artist',
  imports: [],
  templateUrl: './artist.html',
  styleUrl: './artist.css',
})
export class Artist implements OnInit {

  private artistService = inject(ArtistService);
  private route = inject(ActivatedRoute);

  artist = signal<GetArtistByIdDto | null>(null);

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.artistService.getById(id).subscribe({
      next: data => this.artist.set(data),
      error: err => console.log(err)
    });
  }
}
