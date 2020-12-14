import { Component, OnInit } from '@angular/core';
import { WebsiteUsersService } from 'src/app/_api/services';
import { Router } from '@angular/router';
import { WebsiteUser } from 'src/app/_api/models/website-user';

@Component({
  selector: 'app-website-user',
  templateUrl: './website-user.component.html',
  styleUrls: ['./website-user.component.scss']
})
export class WebsiteUserComponent implements OnInit {
  dataavailbale = false;
  welRecords: WebsiteUser[];
  report = 'All Clear!';
  errmsg: string;

  constructor(private svc: WebsiteUsersService, private route: Router) {}

  ngOnInit() {
    this.LoadData();
  }
  LoadData() {
    this.welRecords = null;
    this.report = 'Wait! Going for it...';
    this.svc.GetWebsiteUser().subscribe(
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
  HideUser(id: number) {
    this.welRecords = null;
    this.report = 'Wait! Going for it...';
    this.svc.DeleteWebsiteUser(id).subscribe(
      data => {
        console.log(data);
        this.LoadData();
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
}
