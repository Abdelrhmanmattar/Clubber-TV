import { CommonModule, IMAGE_CONFIG } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import {MatchService} from '../../services/match.service';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';


@Component({
  selector: 'app-match-list',
  standalone: true,
  imports: [RouterLink , RouterLinkActive , CommonModule , FormsModule],
  templateUrl: './match-list.component.html',
  styleUrl: './match-list.component.scss'
})
export class MatchListComponent implements OnInit,OnDestroy {
  matches: any[] = [];
  filter: string = '';
  userName:string|null = null;
  private refreshInterval: any;

  constructor(private matchService: MatchService , private auth:AuthService) {}

  ngOnInit() {
    const user = this.auth.getCurrentUser()['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    if(user){
      this.userName = user;
    }
    else
    {
      this.userName = 'Guest';
    }
/*     this.matchService.getMatches().subscribe((data: any) => {
      this.matches = data.map(
        (match:any)=>({
          ...match,
          status : match.status===0?'Replay':'Live',
        }));
      console.log(this.matches);
    }); */
    this.fetchMatches();
    this.refreshInterval = setInterval(() => {
      this.fetchMatches();
    }, 30000); 
  }

  fetchMatches(): void {
    this.matchService.getMatches().subscribe(
      (data:any) => {
        this.matches = data;
      },
      (error) => {
        console.error('Error fetching matches:', error);
      }
    );
  }
  ngOnDestroy() {
    if (this.refreshInterval) {
      clearInterval(this.refreshInterval);
    }
  }



  onAddToPlaylist(matchId: number) {
  this.matchService.addToPlaylist(matchId).subscribe({
    next: (res) => {
    },
    error: (err) => {
      console.error('Failed to add to playlist', err);
    }
  });
}

}
