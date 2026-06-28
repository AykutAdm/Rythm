import { Component, inject, OnInit, signal } from '@angular/core';
import { AlbumService } from '../../../services/album-service';
import { ActivatedRoute } from '@angular/router';
import { GetAlbumByIdDto } from '../../../models/album-model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-album',
  imports: [CommonModule],
  templateUrl: './album.html',
  styleUrl: './album.css',
})
export class Album implements OnInit {
  private albumService = inject(AlbumService);

  private route = inject(ActivatedRoute);

  album = signal<GetAlbumByIdDto | null>(null);

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.albumService.getById(id).subscribe({
      next: (data) => this.album.set(data),
      error: (err) => console.log(err),
    });
  }
}
