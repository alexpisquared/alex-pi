import { Component, OnInit } from '@angular/core';
import { trigger, state, style, transition, animate } from '@angular/animations';

@Component({
  selector: 'app-open-close',
  animations: [
    trigger('openClose', [
      state(
        'open',
        style({
          // height: '120px',
          // width: '300px',
          // opacity: 1,
          // backgroundColor: '#080' // new CanvasGradient().addColorStop(0.5, '#080')
        })
      ),
      state(
        'closed',
        style({
          // height: '100px',
          width: '580px',
          opacity: 0.5,
          backgroundColor: '#888'
        })
      ),
      transition('open => closed', [animate('.1s')]),
      transition('closed => open', [animate('0.333s')])
    ])
  ],
  templateUrl: './open-close.component.html',
  styleUrls: ['./open-close.component.scss']
})
export class OpenCloseComponent implements OnInit {
  isLightOn = false;

  toggleOpenClose() {
    this.isLightOn = !this.isLightOn;
  }
  constructor() {}

  ngOnInit() {
    setTimeout(() => { // todo: The router does no longer schedule redirect navigation within a setTimeout. Make sure your tests do not rely on this behavior.
      this.isLightOn = true;
    }, 400);
  }
}
