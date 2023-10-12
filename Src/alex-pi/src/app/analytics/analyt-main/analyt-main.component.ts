import { Component, OnInit } from '@angular/core';
import { WebEventLoggerService } from 'src/app/serivce/web-event-logger.service';

@Component({
  selector: 'app-analyt-main',
  templateUrl: './analyt-main.component.html',
  styleUrls: ['./analyt-main.component.scss']
})
export class AnalytMainComponent implements OnInit {
  error = 'no error .. yet';
  constructor(private welSvc: WebEventLoggerService) { }

  ngOnInit() {
    try {
      this.welSvc.logIfProd('ANLT');
      this.error = 'ngOnInit no error ++';
    } catch (err) {
      this.error = `${(err as Error).name}, ${(err as Error).message}`;
    }
  }

  mp3_9() { new Audio('assets\\Media\\Good - Fanfare.9.mp3').play(); }
  mp3_7() { new Audio('assets\\Media\\Good - Fanfare.7.mp3').play(); }
  mp3_5() { new Audio('assets\\Media\\Good - Fanfare.5.mp3').play(); }
  logEvName() {
    try {
      this.error = 'logEvName(AnEN)...';
      this.welSvc.logEvName('AnEN');
      this.error = 'sent ++';
    } catch (err) {
      this.error = `${(err as Error).name}, ${(err as Error).message}`;
    }
  }
  async logIfProd() {
    this.error = 'logNothing()...';
    const report = this.welSvc.logNothing();
    await this.delay(350);
    this.error = report;
  }
  opus32() { new Audio('assets\\Media\\Good - Fanfare.32k.opus').play(); }
  opus() { new Audio('assets\\Media\\Good - Fanfare.opus').play(); }
  wav() { new Audio('assets\\Media\\Good - Fanfare.wav').play(); }

  delay(ms: number) { return new Promise(resolve => setTimeout(resolve, ms)); }
}
