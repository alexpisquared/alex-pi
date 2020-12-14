import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-design-tytu',
  templateUrl: './my-design-tytu.component.html',
  styleUrls: ['./my-design-tytu.component.scss']
})
export class MyDesignTytuComponent implements OnInit {
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
