import { Component, OnInit } from '@angular/core';
import { WebEventLoggerService } from 'src/app/serivce/web-event-logger.service';

@Component({
  selector: 'app-tennis-timer',
  templateUrl: './tennis-timer.component.html',
  styleUrls: ['./tennis-timer.component.scss'],
})
export class TennisTimerComponent implements OnInit {
  everyXMin = 10;
  percentComplete = 0;
  countdownString = '··:··';
  report = '';
  error = 'no error';
  nextTime = new Date();
  nextHHMM = '88:88';
  playPeriods: number[] = [2, 10, 15, 20, 30, 999];
  wakeLock: any;
  soundEffect = new Audio(); // https://stackoverflow.com/questions/31776548/why-cant-javascript-play-audio-files-on-iphone-safari

  constructor(private welSvc: WebEventLoggerService) { }

  async ngOnInit() {
    try {
      this.welSvc.logIfProd('ttmr');
      this.soundEffect.autoplay = true;
      // tslint:disable-next-line:max-line-length
      // this.soundEffect.src = 'data:audio/mpeg;base64,SUQzBAAAAAABEVRYWFgAAAAtAAADY29tbWVudABCaWdTb3VuZEJhbmsuY29tIC8gTGFTb25vdGhlcXVlLm9yZwBURU5DAAAAHQAAA1N3aXRjaCBQbHVzIMKpIE5DSCBTb2Z0d2FyZQBUSVQyAAAABgAAAzIyMzUAVFNTRQAAAA8AAANMYXZmNTcuODMuMTAwAAAAAAAAAAAAAAD/80DEAAAAA0gAAAAATEFNRTMuMTAwVVVVVVVVVVVVVUxBTUUzLjEwMFVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVf/zQsRbAAADSAAAAABVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVf/zQMSkAAADSAAAAABVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV'; // This is a tiny MP3 file that is silent and extremely short
      this.soundEffect.src = 'assets\\Media\\Start - Arcade Alarm.mp3';
      this.error = '+...'; await this.delay(222);
      this.error = '++..'; await this.delay(222);
      this.error = '+++.'; await this.delay(222);
      this.error = '++++'; await this.delay(222);
    } catch (err) {
      this.error = `${(err as Error).name}, ${(err as Error).message}`;
    }

    await this.MainLoop();
  }

  async MainLoop() {
    this.wakeLock = null;

    try {
      this.wakeLock = await navigator.wakeLock.request('screen');
      this.report = 'Wake Lock is active!';
    } catch (err) {
      this.error = `${(err as Error).name}, ${(err as Error).message}`;
    }

    while (true) {
      let now = new Date();
      this.setAndShowNextTime();

      while (now < this.nextTime) {
        const prev = this.everyXMin;
        await this.delay(950);
        if (prev !== this.everyXMin) { // if the user changed the time, then reset the timer
          this.setAndShowNextTime();
        }

        now = new Date();
        const secondsLeft = (this.nextTime.getTime() - now.getTime()) / 1000;
        this.countdownString = Math.floor(secondsLeft / 60) + ':' + ('0' + Math.floor(secondsLeft % 60)).slice(-2);
        this.percentComplete = Math.round((100 * (this.everyXMin * 60 - secondsLeft) / (this.everyXMin * 60)) * 10) / 10;

        if (secondsLeft <= 60 && secondsLeft > 56) { // play a sound when there is 1 minute left
          this.countdownString = '▓▓▓▓';
          await this.playWavFilesAsync(1);
        }
      } // while (now < this.nextTime)

      this.error = '·';
      this.countdownString = '▄▀▄▀';
      await this.playWavFilesAsync(2);
    } // while (true)
  }

  private setAndShowNextTime() {
    const now = new Date();
    this.nextTime = new Date(now.getTime() + (this.everyXMin * 60 * 1000) - (now.getTime() % (this.everyXMin * 60 * 1000)));
    this.nextHHMM = ('0' + this.nextTime.getHours()).slice(-2) + ':' + ('0' + this.nextTime.getMinutes()).slice(-2);
  }

  delay(ms: number) { return new Promise(resolve => setTimeout(resolve, ms)); }

  async playWavFilesAsync(i: number) {
    switch (i) {
      case 1:
        this.playResourse('assets\\Media\\Start - Arcade Power Up.7.mp3');
        await this.delay(2500);
        this.playResourse(this.getLastMinute());
        await this.delay(2000);
        break;
      default:
        this.playResourse('assets\\Media\\Good - Fanfare.7.mp3');
        await this.delay(5000);
        this.playResourse(this.getTimeToChange());
        await this.delay(1000);
        break;
    }
  }

  async releaseWakeLock() {
    if (this.wakeLock !== null) {
      this.wakeLock.release()
        .then(() => {
          this.report = 'Wake Lock released';
          console.log('Wake Lock released');
          this.wakeLock = null;
        });
      this.playResourse('assets\\Media\\en-US-AriaNeural~1.00~100~whispering~Wake Lock released!.7.mp3');
      await this.delay(1000);
    }
  }

  playResourse(filePath: string) { this.soundEffect.src = filePath; }    // new Audio(filePath).play();

  Start_Arcade_Power_Up7mp3() { this.soundEffect.src = 'assets\\Media\\Start - Arcade Power Up.7.mp3'; }  // new Audio('assets\\Media\\Start - Arcade Power Up.7.mp3').play();

  getLastMinute(): string {
    const stringArr = [
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~angry~Last minute! EQAQJ！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~calm~Last minute! EQAQJ！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~cheerful~Last minute! EQAQJ！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~gentle~Last minute! EQAQJ！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~sad~Last minute! EQAQJ！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~serious~Last minute! EQAQJ！.7.mp3'];
    return stringArr[Math.floor(Math.random() * stringArr.length)];
  }

  getTimeToChange(): string {
    const stringArr = [
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~angry~Time to rotate! DYRGOE！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~calm~Time to rotate! DYRGOE！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~cheerful~Time to rotate! DYRGOE！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~gentle~Time to rotate! DYRGOE！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~sad~Time to rotate! DYRGOE！.7.mp3',
      'assets\\Media\\zh-CN-XiaomoNeural~1.00~100~serious~Time to rotate! DYRGOE！.7.mp3'];
    return stringArr[Math.floor(Math.random() * stringArr.length)];
  }

  ngOnDestroy() { this.releaseWakeLock(); }
}
