import { Component, OnInit, OnDestroy, Inject, AfterViewInit, HostBinding } from '@angular/core';
import { Subscription, fromEvent } from 'rxjs';
import { DOCUMENT } from '@angular/common';
import { Router } from '@angular/router';
import { WebEventLoggerService } from 'src/app/serivce/web-event-logger.service';
import { stickyHeaderAnimation, VisibilityState } from 'src/app/animations';
import { throttleTime, map, pairwise, distinctUntilChanged, share, filter } from 'rxjs/operators';

// import { throttleTime, map, pairwise, distinctUntilChanged, share, filter } from 'rxjs/operators';
// import { animate, state, style, transition, trigger } from '@angular/animations';

enum Direction {
  Up = 'Up',
  Down = 'Down'
}

@Component({
  selector: 'app-nav-header',
  templateUrl: './nav-header.component.html',
  styleUrls: ['./nav-header.component.scss'],
  animations: [stickyHeaderAnimation]
})
export class NavHeaderComponent implements OnInit, OnDestroy, AfterViewInit {
  appTitle = 'alexPi';
  titleTla = 'API';
  userName = '[Entered user name]';
  isLoading: boolean; // used for showing Loading spinner during transition between pages
  isLoggingToServer: boolean;
  private subscription: Subscription;
  private themeKey = 'themeKey';
  private mainTheme = 'main-theme';
  private darkTheme = 'dark-theme';
  // AlexPiLogoThemed = './assets/images/AlexTiny_LinkedIn.png';
  AlexPiLogoThemed = './assets/images/AlexPi pro 315x315 - Ukraine Flag - Ofc.png';
  UkraineFlagThemd = './assets/images/UkraineFlag.svg'; // a change to trigger pipeline rebuild
  isSignedIn = false;
  private isMobileResolution: boolean;

  get themeVal(): string {
    let returnValue = localStorage.getItem(this.themeKey);
    if (returnValue === null || returnValue.length < 4) {
      returnValue = this.darkTheme;
    }
    return returnValue;
  }
  set themeVal(theme: string) {
    if (!(theme === null || theme.length < 4)) {
      localStorage.setItem(this.themeKey, theme);
    }
    console.log(` ** theme just set to : ${localStorage.getItem(this.themeKey)}`);
  }

  // https://stackblitz.com/github/zetsnotdead/ng-reactive-sticky-header \\
  private isVisible = true;
  @HostBinding('@toggle')
  get toggle(): VisibilityState {
    return this.isVisible ? VisibilityState.Visible : VisibilityState.Hidden;
  }

  constructor(@Inject(DOCUMENT) private document: Document, public router: Router, private welSvc: WebEventLoggerService) {
    this.isMobileResolution = window.innerWidth < 980;
  }

  ngAfterViewInit() {
    const scroll$ = fromEvent(window, 'scroll').pipe(
      throttleTime(10),
      map(() => window.pageYOffset),
      pairwise(),
      map(([y1, y2]): Direction => (y2 < y1 ? Direction.Up : Direction.Down)),
      distinctUntilChanged(),
      share()
    );
    const goingUp$ = scroll$.pipe(filter(direction => direction === Direction.Up));
    const goingDown$ = scroll$.pipe(filter(direction => direction === Direction.Down));
    goingUp$.subscribe(() => (this.isVisible = true));
    goingDown$.subscribe(() => (this.isVisible = false));
  }
  // https://stackblitz.com/github/zetsnotdead/ng-reactive-sticky-header //

  ngOnInit() {
    this.setThemeToStoredValue();
  }
  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  public getIsMobileResolution(): boolean {
    return this.isMobileResolution;
  }

  signIn(): void {
    this.isSignedIn = true;
    setTimeout(() => { // todo: The router does no longer schedule redirect navigation within a setTimeout. Make sure your tests do not rely on this behavior.
      this.router.navigate(['/']);
    }, 3333);
  }
  signOut(): void {
    this.isSignedIn = false;
    this.router.navigate(['/']);
  }

  toggleTheme() {
    if (this.document.body.classList.contains(this.mainTheme)) {
      this.welSvc.logIfProd('dark');
      this.document.body.classList.remove(this.mainTheme);
      this.document.body.classList.add((this.themeVal = this.darkTheme));
      // this.AlexPiLogoThemed = './assets/images/AlexPi_Logo_Dark.png';
    } else {
      this.document.body.classList.remove(this.darkTheme);
      this.document.body.classList.add((this.themeVal = this.mainTheme));
      // this.AlexPiLogoThemed = './assets/images/AlexPi_Logo_Lite.png';
      this.welSvc.logIfProd('lite');
    }
    console.log(` ** theme toggled  to   ${this.themeVal}`);
  }
  setThemeToStoredValue() {
    console.log(` ** setting to store    ${this.themeVal}`);

    if (this.document.body.classList.contains(this.mainTheme)) {
      this.document.body.classList.remove(this.mainTheme);
    } else if (this.document.body.classList.contains(this.darkTheme)) {
      this.document.body.classList.remove(this.darkTheme);
    }

    this.document.body.classList.add(this.themeVal);

    // this.AlexPiLogoThemed = this.themeVal === this.mainTheme ? './assets/images/AlexPi_Logo_Lite.png' : './assets/images/AlexPi_Logo_Dark.png';
  }
}
