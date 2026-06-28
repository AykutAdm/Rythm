import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreatePlaylistDto, GetPlaylistByIdDto, ResultPlaylistDto, UpdatePlaylistDto } from "../models/playlist-model";

@Injectable({
  providedIn: 'root'
})
export class PlaylistService {

  constructor(private http: HttpClient) {}

  baseUrl: string = 'https://localhost:7190/api/Playlists/';

  getAll() {
    return this.http.get<ResultPlaylistDto[]>(this.baseUrl);
  }

  getById(id: number) {
    return this.http.get<GetPlaylistByIdDto>(this.baseUrl + id);
  }

  create(model: CreatePlaylistDto) {
    return this.http.post(this.baseUrl, model);
  }

  update(model: UpdatePlaylistDto) {
    return this.http.put(this.baseUrl, model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + id);
  }



  getByUserId(userId: number) {
    return this.http.get<ResultPlaylistDto[]>(this.baseUrl + 'user/' + userId);
  }

  addSong(playlistId: number, songId: number) {
    return this.http.post(this.baseUrl + 'add-song', { playlistId, songId, order: 0 });
  }

  removeSong(playlistId: number, songId: number) {
    return this.http.post(this.baseUrl + 'remove-song', { playlistId, songId });
  }
}
