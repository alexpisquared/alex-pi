import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-resumes',
  templateUrl: './my-resumes.component.html',
  styleUrls: ['./my-resumes.component.scss']
})
export class MyResumesComponent implements OnInit {
  cv03page = 'Resume - Alex Pigida - short summary';
  cv11page = 'Resume - Alex Pigida - long detailed version';

  constructor() {}

  ngOnInit() {}
}
