import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CreateListeningHistoryDto, ResultSongDto } from '../models/song.model';

@Injectable({
  providedIn: 'root',
})
export class HistoryService {
  private http = inject(HttpClient);
  baseUrl = 'https://localhost:7190/api/Histories/';

  recordListen(model: CreateListeningHistoryDto) {
    return this.http.post(this.baseUrl, model);
  }
  getRecommendations(userId: number) {
    return this.http.get<ResultSongDto[]>(this.baseUrl + 'recommendations/' + userId);
  }
}
