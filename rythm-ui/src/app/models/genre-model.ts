import { SongSummary } from "./song.model";

export interface ResultGenreDto {
  genreId: number;
  name: string;
}

export interface GetGenreByIdDto {
  genreId: number;
  name: string;
  songs: SongSummary[];
}

export interface CreateGenreDto {
  name: string;
}

export interface UpdateGenreDto {
  genreId: number;
  name: string;
}
