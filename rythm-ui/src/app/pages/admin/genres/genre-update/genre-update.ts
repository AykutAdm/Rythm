import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { GenreService } from '../../../../services/genre-service';
import { UpdateGenreDto } from '../../../../models/genre-model';

@Component({
  selector: 'app-genre-update',
  imports: [FormsModule, RouterLink],
  templateUrl: './genre-update.html',
  styleUrl: './genre-update.css',
})
export class GenreUpdate implements OnInit {

  private genreService = inject(GenreService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private cdr = inject(ChangeDetectorRef);

   model: UpdateGenreDto = {
    genreId: 0,
    name: ''
  }


  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.genreService.getById(id).subscribe({
      next: data => {
        this.model = {
          genreId: data.genreId,
          name: data.name
        };
        this.cdr.detectChanges();
      },
      error: err => console.log(err)
    });
  }

  onSubmit() {
    this.genreService.update(this.model).subscribe({
      complete: () => {
        this.router.navigate(['/admin/genres']);
      },
      error: err => console.log(err)
    });
  }
}
