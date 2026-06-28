import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SongSearchResult } from '../../../models/song.model';
import { SongService } from '../../../services/song-service';
import { AudioService } from '../../../services/audio-service';

@Component({
  selector: 'app-search',
  imports: [FormsModule],
  templateUrl: './search.html',
  styleUrl: './search.css',
})
export class Search {

  private songService = inject(SongService);
  audioService = inject(AudioService);

  query = signal<string>('');
  results = signal<SongSearchResult[]>([]);
  isSearching = signal<boolean>(false);

  onSearch() {
    if (!this.query()) return;
    this.isSearching.set(true);
    this.songService.search(this.query()).subscribe({
      next: data => {
        this.results.set(data);
        this.isSearching.set(false);
      },
      error: () => this.isSearching.set(false)
    });
  }

  onKeyUp(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.onSearch();
    }
  }
}
