import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MatchService {
  private apiUrl = 'https://localhost:7036/api/Match';

  constructor(private http:HttpClient) { }
  getMatches() {
    return this.http.get(`${this.apiUrl}` );
  }
addToPlaylist(matchId: number) {
  return this.http.post(`https://localhost:7036/api/Playlist/${matchId}`,{});
}


}
