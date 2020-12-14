import { Component, OnInit, ViewChild, ElementRef, NgZone, OnDestroy, isDevMode } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';
import { WebEventLoggerService } from '../serivce/web-event-logger.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  constructor(private welSvc: WebEventLoggerService, private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.queryParams.subscribe(queryParams => {
      const rv = queryParams.qaz === 'sms' ? 'Std Mail Sender' : queryParams.qaz === 'sgn' ? 'mail signature' : queryParams.qaz ;
      this.welSvc.logIfProd(`home ${rv}`);
      console.log(` ** queryParams: qaz=${queryParams.qaz}  qwe=${queryParams.qwe}  `);
    });
  }
}
