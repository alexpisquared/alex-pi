import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { VwEventUserUtcsService } from 'src/app/_api/services';
import { VwEventUserUtc } from 'src/app/_api/models';
import { CompInteractService } from 'src/app/serivce/comp-interact.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-vw-event-user-utc',
  templateUrl: './vw-event-user-utc.component.html',
  styleUrls: ['./vw-event-user-utc.component.scss']
})
export class VwEventUserUtcComponent implements OnInit, OnDestroy {
  @Input() astronaut: number;
  userId = -7;
  nickNm = '';
  pcSignre = '';
  confirmed = false;
  announced = false;
  subscription: Subscription;

  isBusy = false;
  dataavailbale = false;
  welRecords: VwEventUserUtc[];
  tempemp: VwEventUserUtc;
  report = 'All Clear!';
  errmsg: string;

  constructor(private svc: VwEventUserUtcsService, private missionService: CompInteractService) {}
  confirm() {
    this.confirmed = true;
    this.missionService.confirmMission(this.astronaut);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe(); // prevent memory leak when component destroyed
  }
  ngOnInit() {
    this.subscription = this.missionService.selectNickNm$.subscribe(nickNm => {
      this.nickNm = nickNm;
      this.LoadData();
    });
    this.subscription = this.missionService.selectUserId$.subscribe(userId => {
      this.userId = userId;
      this.announced = true;
      this.confirmed = false;
    });
    console.log(` ** ${this.userId}  ** ${this.nickNm} ******************`);
    this.LoadData();
  }
  LoadData() {
    this.isBusy = true;
    this.welRecords = null;
    this.report = 'Wait! Going for it...';

    const getter = this.nickNm === '' ? this.svc.GetVwEventUserUtc() : this.svc.GetVwEventUserUtcWithParam({ nickname: this.nickNm, userId: this.userId });
    getter.subscribe(
      data => {
        setTimeout(() => (this.isBusy = false), 250);
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

  deleteconfirmation(id) {
    if (confirm('Are you sure you want to delete this ?')) {
      // this.svc.DeleteVwEventUserUtc(id).subscribe(res => {
      //   alert('Deleted successfully !!!');
      //   this.LoadData();
      // });
    }
  }
  loadAddnew() {
    console.log('loadAddnew() ...');
  }
  loadnewForm(id: string, email: string, firstname: string, lastname: string, gender: number) {
    console.log(gender);
  }
}
