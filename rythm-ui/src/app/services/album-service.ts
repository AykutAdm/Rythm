import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreateAlbumDto, GetAlbumByIdDto, ResultAlbumDto, UpdateAlbumDto } from "../models/album-model";

@Injectable({
  providedIn: 'root'
})
export class AlbumService {

  constructor(private http: HttpClient) {}

  baseUrl: string = 'https://localhost:7190/api/Albums/';

  getAll() {
    return this.http.get<ResultAlbumDto[]>(this.baseUrl);
  }

  getById(id: number) {
    return this.http.get<GetAlbumByIdDto>(this.baseUrl + id);
  }

  create(model: CreateAlbumDto) {
    return this.http.post(this.baseUrl, model);
  }

  update(model: UpdateAlbumDto) {
    return this.http.put(this.baseUrl, model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + id);
  }
}
