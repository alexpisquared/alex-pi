import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({ providedIn: 'root' }) // @Injectable() <= fixes  NullInjectorError: StaticInjectorError[VwEventUserUtcComponent -> CompInteractService]:   NullInjectorError: No provider for CompInteractService!
export class CompInteractService {
  constructor() {}

  // Observable number sources
  private selectUserIdSource = new Subject<number>();
  private selectNickNmSource = new Subject<string>();
  private missionConfirmedSource = new Subject<number>();

  // Observable number streams
  selectUserId$ = this.selectUserIdSource.asObservable(); // todo: one or the other ... but not both! 
  selectNickNm$ = this.selectNickNmSource.asObservable(); // todo: one or the other ... but not both!
  missionConfirmed$ = this.missionConfirmedSource.asObservable();

  // Service message commands
  setUserId(userId: number) {
    this.selectUserIdSource.next(userId);
  }
  setNickNm(nickNm: string) {
    this.selectNickNmSource.next(nickNm);
  }

  confirmMission(astronaut: number) {
    this.missionConfirmedSource.next(astronaut);
  }
}
