import { Component, OnInit } from '@angular/core';
import { WebEventLoggerService } from 'src/app/serivce/web-event-logger.service';
import { MaterialModule } from 'src/app/material/material.module';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-my-designs',
  templateUrl: './my-designs.component.html',
  styleUrls: ['./my-designs.component.scss']
})
export class MyDesignsComponent implements OnInit {
  constructor(private welSvc: WebEventLoggerService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams.subscribe(queryParams => {
      this.welSvc.logEvName(`dsgn:${queryParams.nme}:${queryParams.qaz}`);
    });
  }
}
