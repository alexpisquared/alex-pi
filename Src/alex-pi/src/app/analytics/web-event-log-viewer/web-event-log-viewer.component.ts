import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { WebEventLogsService } from 'src/app/_api/services';
import { WebEventLog } from 'src/app/_api/models/web-event-log';

@Component({
  selector: 'app-web-event-log-viewer',
  templateUrl: './web-event-log-viewer.component.html',
  styleUrls: ['./web-event-log-viewer.component.scss']
})
export class WebEventLogViewerComponent implements OnInit {
  dataavailbale = false;
  welRecords: WebEventLog[];
  tempemp: WebEventLog;
  report = 'All Clear!';
  errmsg: string;

  constructor(private svc: WebEventLogsService, private route: Router) {}

  ngOnInit() {
    this.LoadData();
  }
  LoadData() {
    this.welRecords = null;
    this.report = 'Wait! Going for it...';
    this.svc.GetWebEventLog().subscribe(
      data => {
        this.welRecords = data;
        console.log(this.welRecords);
        if (this.welRecords.length > 0) {
          this.dataavailbale = true;
          this.report = `[Top] ${this.welRecords.length} rows returned.`;
        } else {
          this.report = `0 rows returned.`;
          this.dataavailbale = false;
        }
      },
      err => {
        this.errmsg = err.message
          // .toString()
          // .replace('https', '<br /><br />https')
          // .replace(': ', '<br /><br />')
          ;
        console.log(`@@ err: ${err.message}`);
      }
    );
  }

  deleteconfirmation(id) {
    if (confirm('Are you sure you want to delete this ?')) {
      // this.tempemp = new WebEventLog();
      // this.tempemp.Id = id;
      this.svc.DeleteWebEventLog(id).subscribe(res => {
        alert('Deleted successfully !!!');
        this.LoadData();
      });
    }
  }
  loadAddnew() {
    // this.addcomponent.objemp.email = '';
    // this.addcomponent.objemp.firstname = '';
    // this.addcomponent.objemp.lastname = '';
    // this.addcomponent.objemp.id = '';
    // this.addcomponent.objemp.gender = 0;
  }
  loadnewForm(id: string, email: string, firstname: string, lastname: string, gender: number) {
    console.log(gender);
    // this.editcomponent.objemp.email = email;
    // this.editcomponent.objemp.firstname = firstname;
    // this.editcomponent.objemp.lastname = lastname;
    // this.editcomponent.objemp.id = id;
    // this.editcomponent.objemp.gender = gender;
  }
}
