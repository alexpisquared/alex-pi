import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { WebEventLoggerService } from '../serivce/web-event-logger.service';

@Component({
  selector: 'app-about',
  templateUrl: './about.component.html',
  styleUrls: ['./about.component.scss']
})
export class AboutComponent implements OnInit {
  result = 'Result : nothing ...yet';

  constructor(private location: Location, private welSvc: WebEventLoggerService) {}

  ngOnInit() {
    this.welSvc.logEvName('abou');
    this.getRandom();
  }

  getRandom() {
    this.result = `${this.numberWithCommas(Math.floor(Math.random() * 100000000))}.`;
  }
  numberWithCommas(x) {
    return '123'; // x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
  }
  goBack(): void {
    this.location.back();
  }
}
