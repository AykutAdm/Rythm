export interface ResultSongDto {
  songId: number;
  title: string;
  coverImageUrl: string;
  audioUrl: string;
  durationInSeconds: number;
  playCount: number;
  releaseDate: string;
  artistName: string;
  albumTitle: string;
  genreName: string;
  requiredPlan: string;
}

export interface GetSongByIdDto {
  songId: number;
  title: string;
  coverImageUrl: string;
  audioUrl: string;
  durationInSeconds: number;
  playCount: number;
  releaseDate: string;
  artistId: number;
  artistName: string;
  albumId: number;
  albumTitle: string;
  genreId: number;
  genreName: string;
  requiredPlan: string;
}

export interface CreateSongDto {
  title: string;
  coverImageUrl: string;
  audioUrl: string;
  durationInSeconds: number;
  releaseDate: string;
  artistId: number;
  albumId: number;
  genreId: number;
  requiredPlan: string;
}

export interface UpdateSongDto {
  songId: number;
  title: string;
  coverImageUrl: string;
  audioUrl: string;
  durationInSeconds: number;
  playCount: number;
  releaseDate: string;
  artistId: number;
  albumId: number;
  genreId: number;
  requiredPlan: string;
}

export interface SongSearchResult {
  songId: number;
  title: string;
  artistName: string;
  albumTitle: string;
  genreName: string;
  coverImageUrl: string;
  audioUrl: string;
}

export interface SongSummary {
  songId: number;
  title: string;
  coverImageUrl: string;
}

export interface CreateListeningHistoryDto {
  appUserId: number;
  songId: number;
}
