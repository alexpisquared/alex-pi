import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { Component, OnInit, ViewChild, ElementRef, NgZone, OnDestroy } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { HomeComponent } from './home.component';
import { MaterialModule } from '../material/material.module';
import { MyStatusOpenComponent } from '../my/my-status-open/my-status-open.component';
import { MyResumesComponent } from '../my/my-resumes/my-resumes.component';
import { MyClientsComponent } from '../my/my-clients/my-clients.component';
import { NavFooterComponent } from '../nav/nav-footer/nav-footer.component';
import { HttpClientModule } from '@angular/common/http';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [RouterTestingModule, MaterialModule, BrowserAnimationsModule, HttpClientModule],
      declarations: [HomeComponent, MyStatusOpenComponent, MyResumesComponent, MyClientsComponent, NavFooterComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
