import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CreateGenreDto, GetGenreByIdDto, ResultGenreDto, UpdateGenreDto } from "../models/genre-model";

@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(private http: HttpClient) {}

  baseUrl: string = 'https://localhost:7190/api/Genres/';

  getAll() {
    return this.http.get<ResultGenreDto[]>(this.baseUrl);
  }

  getById(id: number) {
    return this.http.get<GetGenreByIdDto>(this.baseUrl + id);
  }

  create(model: CreateGenreDto) {
    return this.http.post(this.baseUrl, model);
  }

  update(model: UpdateGenreDto) {
    return this.http.put(this.baseUrl, model);
  }

  delete(id: number) {
    return this.http.delete(this.baseUrl + id);
  }
}
