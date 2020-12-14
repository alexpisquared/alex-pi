import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-design-str2',
  templateUrl: './my-design-str2.component.html',
  styleUrls: ['./my-design-str2.component.scss']
})
export class MyDesignStr2Component implements OnInit {
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
