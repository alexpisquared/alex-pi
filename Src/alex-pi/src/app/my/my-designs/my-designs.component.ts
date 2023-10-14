import { Component, OnInit } from '@angular/core';
import { WebEventLoggerService } from 'src/app/serivce/web-event-logger.service';
import { MaterialModule } from 'src/app/material/material.module';

@Component({
  selector: 'app-my-designs',
  templateUrl: './my-designs.component.html',
  styleUrls: ['./my-designs.component.scss']
})
export class MyDesignsComponent implements OnInit {
  constructor(private welSvc: WebEventLoggerService) {}

  ngOnInit() {
    this.welSvc.logEvName('dsgn');
  }
}
