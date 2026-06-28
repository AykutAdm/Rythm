import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreateArtistDto, GetArtistByIdDto, ResultArtistDto, UpdateArtistDto } from "../models/artist-model";

@Injectable({
  providedIn: 'root'
})
export class ArtistService {

  constructor(private http: HttpClient) {}

  baseUrl: string = 'https://localhost:7190/api/Artists/';

  getAll() {
    return this.http.get<ResultArtistDto[]>(this.baseUrl);
  }

  getById(id: number) {
    return this.http.get<GetArtistByIdDto>(this.baseUrl + id);
  }

  create(model: CreateArtistDto) {
    return this.http.post(this.baseUrl, model);
  }

  update(model: UpdateArtistDto) {
    return this.http.put(this.baseUrl, model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + id);
  }
}
