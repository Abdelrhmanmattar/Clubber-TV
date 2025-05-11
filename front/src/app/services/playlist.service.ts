import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root'
})
export class PlaylistService {
  private baseUrl = 'https://localhost:7036/api/Playlist';

  constructor(private http: HttpClient) {}

  getPlaylist(){
    return this.http.get(`${this.baseUrl}`);
  }

  removeMatch(idMatch: number) {
    return this.http.delete(`${this.baseUrl}/${idMatch}`);
  }
}
