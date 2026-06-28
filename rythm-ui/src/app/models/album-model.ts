import { SongSummary } from "./song.model";

export interface ResultAlbumDto {
  albumId: number;
  title: string;
  coverImageUrl: string;
  releaseDate: string;
  artistName: string;
}

export interface GetAlbumByIdDto {
  albumId: number;
  title: string;
  coverImageUrl: string;
  releaseDate: string;
  artistName: string;
  songs: SongSummary[];
}

export interface CreateAlbumDto {
  title: string;
  coverImageUrl: string;
  releaseDate: string;
  artistId: number;
}

export interface UpdateAlbumDto {
  albumId: number;
  title: string;
  coverImageUrl: string;
  releaseDate: string;
}

export interface AlbumSummary {
  albumId: number;
  title: string;
  coverImageUrl:string;
}

