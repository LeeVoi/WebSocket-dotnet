import { Component } from '@angular/core';
import {FormControl} from "@angular/forms";
import {BaseDto, ServerEchoClientDto} from "../BaseDto";

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
      const messageFromServer = JSON.parse(message.data) as BaseDto<any>
      // @ts-ignore
      this[messageFromServer.eventType].call(this, messageFromServer);

    }
  }

  ServerEchoClient(dto: ServerEchoClientDto) {
    this.messages.push(dto.echoValue!);
  }
  sendMessage() {
    var object :{eventType: string, messageContent: string} = {
      eventType: "ClientWantsToEchoServer",
      messageContent: this.messageContent.value!
    }
    this.ws.send(JSON.stringify(object));

  }
}
