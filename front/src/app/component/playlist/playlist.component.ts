import { Component, OnInit } from '@angular/core';
import { PlaylistService } from '../../services/playlist.service';
import { AuthService } from '../../services/auth.service';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-playlist',
  standalone: true,
  imports: [RouterLink,RouterLinkActive,CommonModule,FormsModule],
  templateUrl: './playlist.component.html',
  styleUrl: './playlist.component.scss'
})
export class PlaylistComponent implements OnInit {
  playlist: any[] = [];
  loading: boolean = false;
  error: string = '';

  constructor(private auth:AuthService,private playlistService: PlaylistService) {}

  ngOnInit(): void {
    this.playlistService.getPlaylist().subscribe((data: any) => {
      if (data && data.length > 0) {
        this.playlist = data.map(
          (match: any) => ({
            ...match,
            status: match.status === 0 ? 'Replay' : 'Live',
          })
        );
        this.loading = true;
      }
      else{
        this.playlist = [];
        this.loading = false;
        this.error = 'No matches found in the playlist.';
      }

    });
  }


  removeMatch(id: number): void {
    this.playlistService.removeMatch(id).subscribe({
      next: () => {
        this.playlist = this.playlist.filter(match => match.id !== id);
      },
      error: () => {
        this.error = 'Failed to remove match.';
      }
    });
  }
}
