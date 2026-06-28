import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ArtistService } from '../../../../services/artist-service';
import { UpdateArtistDto } from '../../../../models/artist-model';

@Component({
  selector: 'app-artist-update',
  imports: [FormsModule, RouterLink],
  templateUrl: './artist-update.html',
  styleUrl: './artist-update.css',
})
export class ArtistUpdate implements OnInit {

  private artistService = inject(ArtistService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  private cdr = inject(ChangeDetectorRef);


   model: UpdateArtistDto = {
    artistId: 0,
    name: '',
    bio: '',
    profileImageUrl: ''
  }

   ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.artistService.getById(id).subscribe({
      next: data => {
        this.model = {
          artistId: data.artistId,
          name: data.name,
          bio: data.bio,
          profileImageUrl: data.profileImageUrl
        };
        this.cdr.detectChanges();
      },
      error: err => console.log(err)
    });
  }

  onSubmit() {
    this.artistService.update(this.model).subscribe({
      complete: () => {
        this.router.navigate(['/admin/artists']);
      },
      error: err => console.log(err)
    });
  }

}
