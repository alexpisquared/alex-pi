import { Component, OnInit } from '@angular/core';
import { VwUserHopsUtcsService } from 'src/app/_api/services';
import { VwUserHopsUtc } from 'src/app/_api/models/vw-user-hops-utc';
import { Router } from '@angular/router';
import { CompInteractService } from 'src/app/serivce/comp-interact.service';

@Component({
  selector: 'app-vw-user-hops-utc',
  templateUrl: './vw-user-hops-utc.component.html',
  styleUrls: ['./vw-user-hops-utc.component.scss']
})
export class VwUserHopsUtcComponent implements OnInit {
  isBusy = false;
  dataavailbale = false;
  welRecords: VwUserHopsUtc[];
  report = 'All Clear!';
  errmsg: string;

  constructor(private svc: VwUserHopsUtcsService, private interactSvc: CompInteractService, public router: Router) {}

  ngOnInit() {
    this.LoadData();
  }
  LoadData() {
    this.isBusy = true;
    this.welRecords = null;
    this.report = 'Wait! Going for it...';
    this.svc.GetVwUserHopsUtc().subscribe(
      data => {
        this.welRecords = data;
        setTimeout(() => (this.isBusy = false), 250);
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
        setTimeout(() => (this.isBusy = false), 250);
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
    this.isBusy = true;
    this.welRecords = null;
    this.report = 'Wait! Going for it...';
    this.svc.DeleteVwUserHopsUtc(id).subscribe(
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
  goToUserDetail(id: number) {
    this.router.navigate([`/usr/${id}`]);
  }
}
