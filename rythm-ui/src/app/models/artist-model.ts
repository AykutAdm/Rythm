import { AlbumSummary } from "./album-model";
import { SongSummary } from "./song.model";

export interface ResultArtistDto {
  artistId: number;
  name: string;
  bio: string;
  profileImageUrl: string;
}

export interface GetArtistByIdDto {
  artistId: number;
  name: string;
  bio: string;
  profileImageUrl: string;
  albums: AlbumSummary[];
  songs: SongSummary[];
}

export interface CreateArtistDto {
  name: string;
  bio: string;
  profileImageUrl: string;
}

export interface UpdateArtistDto {
  artistId: number;
  name: string;
  bio: string;
  profileImageUrl: string;
}
