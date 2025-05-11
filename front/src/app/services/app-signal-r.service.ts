import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AppSignalRService {
  private hubConnection: signalR.HubConnection;
  private messageSubject = new Subject<string>();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7036/Noti',{accessTokenFactory: () => localStorage.getItem('token') || ''})
      .withAutomaticReconnect()
      .build();

    // Register the handler once during construction
    this.hubConnection.on('ReceiveMessage', (message: string) => {
      console.log('Received from SignalR:', message);
      this.messageSubject.next(message);
    });
  }

  startConnection(): Observable<void> {
    return new Observable<void>((observer) => {
      this.hubConnection
        .start()
        .then(() => {
          console.log('SignalR connection established');
          observer.next();
          observer.complete();
        })
        .catch((error) => {
          console.error('SignalR connection error:', error);
          observer.error(error);
        });
    });
  }

  receiveMessage(): Observable<string> {
    return this.messageSubject.asObservable();
  }

  sendMessage(message: string): void {
    this.hubConnection.invoke('Sendmessage', message).catch(console.error);
  }
}
