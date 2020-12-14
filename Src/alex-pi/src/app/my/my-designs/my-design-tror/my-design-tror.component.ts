import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-design-tror',
  templateUrl: './my-design-tror.component.html',
  styleUrls: ['./my-design-tror.component.scss']
})
export class MyDesignTrorComponent implements OnInit {
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
