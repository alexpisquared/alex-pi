import { Component, HostBinding } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { slideInAnimation } from './animations';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  animations: [slideInAnimation]
})
export class AppComponent {
  title = 'alex-pi';

  constructor(private router: Router) {}

  prepareRoute(outlet: RouterOutlet) {
    return outlet && outlet.activatedRouteData && outlet.activatedRouteData.animation;
  }
}

/*
PWA solution
https://stackoverflow.com/questions/52536154/angular-6-pwa-the-pwa-functionality-is-interefering-with-azure-adal-authentic
It seems that writing a function to check for new version of the PWA has cleaned up everything. Because it's a PWA, when replacing files with a new version -- the cache will still be there and shift+reloading won't necessarily clear it, causing a lot of unwanted behaviour.

The code for the cleanup looks like this:

First, inject in the constructor the following: updates: SwUpdate

import { SwUpdate } from "@angular/service-worker"

Then, inside ngOnInit, I have the following:

updates.available.subscribe(event => {
      updates.activateUpdate().then(() => document.location.reload());
    })

It will force a complete refresh 2-3 seconds in if there's a new version available but all works well afterwards.-
*/
