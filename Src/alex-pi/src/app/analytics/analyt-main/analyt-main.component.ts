import { Component, OnInit } from '@angular/core';
import { WebEventLoggerService } from 'src/app/serivce/web-event-logger.service';

@Component({
  selector: 'app-analyt-main',
  templateUrl: './analyt-main.component.html',
  styleUrls: ['./analyt-main.component.scss']
})
export class AnalytMainComponent implements OnInit {
  constructor(private welSvc: WebEventLoggerService) { }

  ngOnInit() { this.welSvc.logIfProd('ANLT'); }

  mp3_9() { new Audio('assets\\Media\\Good - Fanfare.9.mp3').play(); }
  mp3_7() { new Audio('assets\\Media\\Good - Fanfare.7.mp3').play(); }
  mp3_5() { new Audio('assets\\Media\\Good - Fanfare.5.mp3').play(); }
  aac32() { new Audio('assets\\Media\\Good - Fanfare.32k.aac').play(); }
  aac() { new Audio('assets\\Media\\Good - Fanfare.aac').play(); }
  opus08() { new Audio('assets\\Media\\Good - Fanfare.8k.opus').play(); }
  opus16() { new Audio('assets\\Media\\Good - Fanfare.16k.opus').play(); }
  opus32() { new Audio('assets\\Media\\Good - Fanfare.32k.opus').play(); }
  opus() { new Audio('assets\\Media\\Good - Fanfare.opus').play(); }
  wav() { new Audio('assets\\Media\\Good - Fanfare.wav').play(); }
}
