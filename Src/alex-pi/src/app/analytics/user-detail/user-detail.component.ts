import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { WebsiteUsersService } from 'src/app/_api/services';
import { WebsiteUser } from 'src/app/_api/models';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CompInteractService } from 'src/app/serivce/comp-interact.service';
import { WebEventLoggerService } from 'src/app/serivce/web-event-logger.service';

@Component({
  selector: 'app-user-detail',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.scss']
})
export class UserDetailComponent implements OnInit {
  @Input() hero: WebsiteUser;

  constructor(private route: ActivatedRoute, private welSvc: WebEventLoggerService, private svc: WebsiteUsersService, private interactSvc: CompInteractService, private location: Location) { }

  ngOnInit(): void {
    this.welSvc.logEvName('uDtl ☺ ☻');
    this.getHero();
  }

  getHero(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.svc.GetWebsiteUser_1(id).subscribe(hero => {
      this.hero = hero;
      this.interactSvc.setNickNm(hero.nickname);
    });
    this.interactSvc.setUserId(id);
  }

  goBack(): void {
    this.location.back();
  }

  save(): void {
    this.svc.PutWebsiteUser({ id: this.hero.id, websiteUser: this.hero }).subscribe(() => this.goBack());
  }
}
