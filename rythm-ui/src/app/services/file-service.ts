import { HttpClient } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class FileService {

  private http = inject(HttpClient);

  baseUrl: string = 'https://localhost:7190/api/File/';

  uploadAudio(file: File) {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<{ url: string }>(this.baseUrl + 'upload-audio', formData);
  }

  uploadImage(file: File) {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<{ url: string }>(this.baseUrl + 'upload-image', formData);
  }
}
