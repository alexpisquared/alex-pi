import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-design-fptt',
  templateUrl: './my-design-fptt.component.html',
  styleUrls: ['./my-design-fptt.component.scss']
})
export class MyDesignFpttComponent implements OnInit {
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
