import { Component } from '@angular/core';
import {FormControl} from "@angular/forms";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'frontend';

  messages: string[] = [];

  ws: WebSocket = new WebSocket("ws://localhost:8181");

  messageContent = new FormControl('')

  constructor() {
    this.ws.onmessage = message => {
      this.messages.push(message.data);
    }
  }
  sendMessage() {
    this.ws.send(this.messageContent.value!);
  }
}
