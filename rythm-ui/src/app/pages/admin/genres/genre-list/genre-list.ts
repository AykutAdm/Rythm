import { Component, inject, OnInit, signal } from '@angular/core';
import { GenreService } from '../../../../services/genre-service';
import { ResultGenreDto } from '../../../../models/genre-model';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-genre-list',
  imports: [RouterLink],
  templateUrl: './genre-list.html',
  styleUrl: './genre-list.css',
})
export class GenreList implements OnInit {

  private genreService = inject(GenreService);

  genres = signal<ResultGenreDto[]>([]);

   ngOnInit() {
    this.load();
  }

  load() {
    this.genreService.getAll().subscribe(data => {
      this.genres.set(data);
    });
  }

  delete(id: number) {
    if (confirm('Silmek istediğinize emin misiniz?')) {
      this.genreService.delete(id).subscribe({
        next: () => {
          this.genres.update(list => list.filter(g => g.genreId !== id));
        },
        error: (err) => {console.error('Silme işlemi başarısız:', err);
        }
      });
    }
  }
}
