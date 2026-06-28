import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AlbumService } from '../../../../services/album-service';
import { ResultAlbumDto } from '../../../../models/album-model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-album-list',
  imports: [CommonModule,RouterLink],
  templateUrl: './album-list.html',
  styleUrl: './album-list.css',
})
export class AlbumList implements OnInit {

  private albumService = inject(AlbumService);

   albums = signal<ResultAlbumDto[]>([]);

    ngOnInit() {
    this.load();
  }

  load() {
    this.albumService.getAll().subscribe(data => {
      this.albums.set(data);
    });
  }

  delete(id: number) {
    if (confirm('Silmek istediğinize emin misiniz?')) {
      this.albumService.delete(id).subscribe({
        next: () => {
          this.albums.update(list => list.filter(a => a.albumId !== id));
        },
        error: (err) => {console.error('Silme işlemi başarısız oldu:', err);
        }
      });
    }
  }
}
