import { Component, OnInit } from '@angular/core';
import { WebEventLoggerService } from 'src/app/serivce/web-event-logger.service';

@Component({
  selector: 'app-analyt-main',
  templateUrl: './analyt-main.component.html',
  styleUrls: ['./analyt-main.component.scss']
})
export class AnalytMainComponent implements OnInit {
  constructor(private welSvc: WebEventLoggerService) {}

  ngOnInit() {
    this.welSvc.logIfProd('ANLT');
  }
  LoadData() {}
}
