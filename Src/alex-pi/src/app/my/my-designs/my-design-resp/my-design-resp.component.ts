import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-design-resp',
  templateUrl: './my-design-resp.component.html',
  styleUrls: ['./my-design-resp.component.scss']
})
export class MyDesignRespComponent implements OnInit {
  step = 0;
  constructor() {}

  ngOnInit() {}

  setStep(index: number) {
    this.step = index;
  }

  nextStep() {
    this.step++;
  }

  prevStep() {
    this.step--;
  }
}
