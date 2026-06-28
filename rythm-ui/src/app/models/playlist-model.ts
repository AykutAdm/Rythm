import { ResultSongDto } from "./song.model";

export interface ResultPlaylistDto {
  playlistId: number;
  name: string;
  description: string;
  coverImageUrl: string;
  isPublic: boolean;
}

export interface GetPlaylistByIdDto {
  playlistId: number;
  name: string;
  description: string;
  coverImageUrl: string;
  isPublic: boolean;
  songs: ResultSongDto[];
}

export interface CreatePlaylistDto {
  name: string;
  description: string;
  coverImageUrl: string;
  isPublic: boolean;
  appUserId: number;
}

export interface UpdatePlaylistDto {
  id: number;
  name: string;
  description: string;
  coverImageUrl: string;
  isPublic: boolean;
}
