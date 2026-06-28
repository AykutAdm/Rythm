import { Component, inject } from '@angular/core';
import { ArtistService } from '../../../../services/artist-service';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { CreateArtistDto } from '../../../../models/artist-model';

@Component({
  selector: 'app-artist-create',
  imports: [FormsModule,RouterLink],
  templateUrl: './artist-create.html',
  styleUrl: './artist-create.css',
})
export class ArtistCreate {

  private artistService = inject(ArtistService);
  private router = inject(Router);

  model: CreateArtistDto = {
    name: '',
    bio: '',
    profileImageUrl: ''
  }

  onSubmit() {
    this.artistService.create(this.model).subscribe({
      complete: () => {
        this.router.navigate(['/admin/artists']);
      },
      error: (err) => console.log(err)
    });
  }
}
