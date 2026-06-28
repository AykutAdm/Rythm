import { Injectable } from "@angular/core";
import { UserService } from "./user-service";
import { AuthService } from "./auth-service";

@Injectable({
  providedIn: 'root'
})
export class LikedSongsService {

  constructor(
    private userService: UserService,
    private authService: AuthService
  ) {}

  likedSongIds: number[] = [];

  load() {

    const userId = this.authService.getUserId();

    this.userService.getLikedSongs(userId).subscribe(result => {

      this.likedSongIds = result.map(x => x.songId);

    });

  }

  isLiked(songId: number): boolean {

    return this.likedSongIds.includes(songId);

  }

  toggleLike(songId: number) {

    const userId = this.authService.getUserId();

    if (this.isLiked(songId)) {

      this.userService.unlikeSong(userId, songId).subscribe(() => {

        this.likedSongIds =
          this.likedSongIds.filter(id => id != songId);

      });

    }
    else {

      this.userService.likeSong(userId, songId).subscribe(() => {

        this.likedSongIds.push(songId);

      });

    }

  }

}
