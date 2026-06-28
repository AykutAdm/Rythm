import { HttpClient } from "@angular/common/http";
import { CreateSongDto, GetSongByIdDto, ResultSongDto, SongSearchResult, UpdateSongDto } from "../models/song.model";
import { Injectable } from "@angular/core";


@Injectable({
  providedIn: 'root'
})
export class SongService {

  constructor(private http: HttpClient) {}

  baseUrl: string = 'https://localhost:7190/api/Songs/';

  getAll() {
    return this.http.get<ResultSongDto[]>(this.baseUrl);
  }

  getById(id: number) {
    return this.http.get<GetSongByIdDto>(this.baseUrl + id);
  }

  search(query: string) {
    return this.http.get<SongSearchResult[]>(this.baseUrl + 'search?query=' + query);
  }

  create(model: CreateSongDto) {
    return this.http.post(this.baseUrl, model);
  }

  update(model: UpdateSongDto) {
    return this.http.put(this.baseUrl, model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + id);
  }
}
