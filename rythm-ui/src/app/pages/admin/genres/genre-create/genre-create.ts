import { Component, inject } from '@angular/core';
import { GenreService } from '../../../../services/genre-service';
import { Router, RouterLink } from '@angular/router';
import { CreateGenreDto } from '../../../../models/genre-model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-genre-create',
  imports: [FormsModule, RouterLink],
  templateUrl: './genre-create.html',
  styleUrl: './genre-create.css',
})
export class GenreCreate {

  private genreService = inject(GenreService);
  private router = inject(Router);

   model: CreateGenreDto = {
    name: ''
  }

  onSubmit() {
    this.genreService.create(this.model).subscribe({
      complete: () => {
        this.router.navigate(['/admin/genres']);
      },
      error: err => console.log(err)
    });
  }
}
