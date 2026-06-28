import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { SongService } from '../../../../services/song-service';
import { ResultSongDto } from '../../../../models/song.model';

@Component({
  selector: 'app-song-list',
  imports: [RouterLink, CommonModule],
  templateUrl: './song-list.html',
  styleUrl: './song-list.css',
})
export class SongList implements OnInit {

  private songService = inject(SongService);

  songs = signal<ResultSongDto[]>([]);

  ngOnInit() {
    this.load();
  }

  load() {
    this.songService.getAll().subscribe(data => {
      this.songs.set(data);
    });
  }

  delete(id: number) {
    if (confirm('Silmek istediğinize emin misiniz?')) {
      this.songService.delete(id).subscribe({
        next: () => {
          this.songs.update(list => list.filter(s => s.songId !== id));
        },
        error: (err) => {console.error('Şarkı silinirken hata oluştu:', err);
        }
      });
    }
  }
}
