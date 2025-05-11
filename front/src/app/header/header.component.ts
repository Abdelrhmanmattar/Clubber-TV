import { Component, OnInit, OnDestroy } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppSignalRService } from '../services/app-signal-r.service';

@Component({
  selector: 'app-header',
  standalone:true,
  imports: [RouterLink , RouterLinkActive,CommonModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent implements OnInit {
  receivedMessage: string = '';
  showNotifications: boolean = false;

  constructor(private signalRService: AppSignalRService) {}

  ngOnInit(): void {
    this.signalRService.startConnection().subscribe(() => {
      this.signalRService.receiveMessage().subscribe((message) => {
        this.receivedMessage = message;
      });
    });
  }
  toggleNotifications(): void {
    this.showNotifications = !this.showNotifications;
  }
}