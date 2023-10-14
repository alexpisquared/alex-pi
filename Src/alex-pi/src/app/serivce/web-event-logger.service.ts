// @ts-nocheck
import { Injectable, isDevMode } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { WebEventLog } from '../model/WebEventLog';
import { WebViewer } from '../model/WebViewer';

@Injectable({
  providedIn: 'root'
})
export class WebEventLoggerService {
  // private svcurl = 'https://localhost:5001'; //
  private svcurl = isDevMode() ? 'https://localhost:5001' : 'https://alex-pi-api.azurewebsites.net'; // always returns prod url>!>!>!?!?!?!
  const pieceOfCake = 'LocalStoreTest';

  constructor(private http: HttpClient) { }

  getUnmaskedInfo(gl2) {
    try {
      // gl2: WebGLRenderingContext does not compile, but useful to debug.
      const unMaskedInfo = {
        renderer: '', // only this is somewhat useful: RAZER looks like: "GPU:ANGLE (NVIDIA GeForce GTX 1070 with Max-Q Design Direct3D11 vs_5_0 ps_5_0)"
        vendor__: ''
        // ...
      };

      const dbgRenderInfo = gl2.getExtension('WEBGL_debug_renderer_info');
      if (dbgRenderInfo != null) {
        unMaskedInfo.renderer = gl2.getParameter(dbgRenderInfo.UNMASKED_RENDERER_WEBGL);
        unMaskedInfo.vendor__ = gl2.getParameter(dbgRenderInfo.UNMASKED_VENDOR_WEBGL);
        // ...
      }

      // for (let i = 37400; i < 37488; i++) {
      //   const v = gl2.getParameter(i);
      //   if (v === null) {
      //     // console.log(` ▄▀ ${i}  `);
      //   } else {
      //     console.log(` ▄▀ ${i}   ${v}`);
      //   }
      // }

      return unMaskedInfo;
    } catch (err) {
      console.log(`ERROR in getUnmaskedInfo(): ${(err as Error).name}, ${(err as Error).message}`);
      return (`ERROR in getUnmaskedInfo(): ${(err as Error).name}, ${(err as Error).message}`);
    }
  }
  getNothing0(): string { return (`▄▀▄▀▄▀`); }
  getNothing1(): string {
    try {
      const canvas = new OffscreenCanvas(512, 512);
      const gl1 = canvas.getContext('webgl');
      const gpr = this.getUnmaskedInfo(gl1).renderer;
      const gpv = this.getUnmaskedInfo(gl1).vendor__;
      const usa = navigator.userAgent.replace('Mozilla/5.0 (', '').replace(') AppleWebKit/537.36 (KHTML, like Gecko) Chrome/', ' ');
      const usl = usa.length - 13; // ... Safari/537.36

      const clientId =
        `${usa.substring(0, 27)}│${usa.substring(usl)}│cpu:${navigator.hardwareConcurrency.toString().padStart(2, '00')}│`+
        `${window.screen.width}x${window.screen.height}xd:${window.screen.colorDepth}│${Intl.DateTimeFormat().resolvedOptions().timeZone}│s:${window.sessionStorage.length}│${gpr}│${gpv}│`+
        `b:${navigator.platform}│p:${navigator.plugins.length}│c:${navigator.cookieEnabled}│${navigator.language}│${navigator.languages}█`;

      return clientId;
    } catch (err) {
      console.log(`ERROR in getNothing1(): ${(err as Error).name}, ${(err as Error).message}`);
      return (`ERROR in getNothing1(): ${(err as Error).name}, ${(err as Error).message}`);
    }
  }

  logEvName1(evname: string): string {
    try {
      const anyString = 'No LocalStorage engagement yet.'

      const evdata = this.getNothing0();
      this.logEvent(anyString, evname, evdata);
      console.log(` ▄▀ prd mode: logged evdata to db`);
      return evdata + '═══' + anyString;
    } catch (err) {
      console.log(`${(err as Error).name}, ${(err as Error).message}`);
      return (`ERROR in logEvName(): ${(err as Error).name}, ${(err as Error).message}`);
    }
  }
  logEvName2(evname: string): string {
    try {
      const anyString = this.getMemberSince();
      const evdata = this.getNothing0();
      this.logEvent(anyString, evname, evdata);
      console.log(` ▄▀ prd mode: logged evdata to db`);
      return evdata + '═══' + anyString;
    } catch (err) {
      console.log(`${(err as Error).name}, ${(err as Error).message}`);
      return (`ERROR in logEvName(): ${(err as Error).name}, ${(err as Error).message}`);
    }
  }
  logEvName(evname: string): string {
    try {
      const anyString = this.getMemberSince();
      const evdata = this.getNothing1();   //////////////////// const evdata = this.getNothing0(); // todo:
      this.logEvent(anyString, evname, evdata);
      console.log(` ▄▀ prd mode: logged evdata to db`);
      return evdata;
    } catch (err) {
      console.log(`${(err as Error).name}, ${(err as Error).message}`);
      return (`ERROR in logEvName(): ${(err as Error).name}, ${(err as Error).message}`);
    }
  }

  private getMemberSince(): string {
    try {
      let anyString = localStorage.getItem(this.pieceOfCake);
      if (anyString === null) {
        anyString = new Date().toString();
        localStorage.setItem(this.pieceOfCake, anyString);
      }
      return anyString;
    } catch (err) {
      console.log(`${(err as Error).name}, ${(err as Error).message}`);
      return (`ERROR in getMemberSince(): ${(err as Error).name}, ${(err as Error).message}`);
    }
  }

  logEvent(noValue: string, evname: string, evdata: string): string {
    this.addEvent(noValue, evname, evdata).subscribe(
      data => {
        console.log(` ▄▀ SUCCESS adding event to db:  ${data} at ${this.svcurl}`);
      },
      err => {
        console.log(` ▄▀▄▀▄▀ ERR in logEvent(${evname}) at ${this.svcurl}:  ${err.message}`);
      }
    );
  }

  addEvent(noValue: string, evname: string, evdata: string): Observable<WebEventLog> {
    const wel = new WebEventLog();
    wel.firstVisitId = noValue;
    wel.eventName = evname;
    wel.browserSignature = evdata;

    return this.insertWebEventLog(wel);
  }
  getAllWebEventLogs0() {
    return this.http.get(`${this.getWebEventLogsUrl()}`);
  }
  getAllWebEventLogs(): Observable<WebEventLog[]> {
    return this.http.get<WebEventLog[]>(`${this.getWebEventLogsUrl()}`);
  }
  getWebEventLog(Id: string): Observable<WebEventLog> {
    return this.http.get<WebEventLog>(`${this.getWebEventLogsUrl()}/${Id}`);
  }
  insertWebEventLog(wel: WebEventLog): Observable<WebEventLog> {
    return this.http.post<WebEventLog>(`${this.getWebEventLogsUrlForLogging()}/`, wel);
  }
  updateWebEventLog(wel: WebEventLog): Observable<void> {
    return this.http.put<void>(`${this.getWebEventLogsUrl()}/${wel.id}`, wel);
  }
  deleteWebEventLog(Id: string) {
    return this.http.delete(`${this.getWebEventLogsUrl()}/${Id}`);
  }

  getAllWebViewers(): Observable<WebViewer[]> {
    return this.http.get<WebViewer[]>(this.getWebViewersUrl());
  }

  getOcr0(): Observable<string> {
    return this.http.get<string>(`${this.getOcrWithLogging()}`);
  }
  getOcr2(imgurl2: string): Observable<string> {
    const httpOptions = {
      headers: { 'Content-Type': 'application/json' },
      params: { imgurl: imgurl2 }
    };
    return this.http.get<string>(`${this.getOcrWithLogging()}/${imgurl2}`);
    // return this.http.get<string>(`${this.getOcrWithLogging()}?imgurl=${imgurl2}`, httpOptions);
    // return this.http.get<string>(`${this.getOcrWithLogging()}`, httpOptions);
  }

  getOcrWithLogging() {
    return `${this.svcurl}/api/ocr`;
  }
  getWebEventLogsUrlForLogging() {
    return `${this.svcurl}/api/WebEventLogs`;
  }
  getWebEventLogsUrl(): string {
    return `${this.svcurl}/api/WebEventLogs`;
  }
  getWebViewersUrl(): string {
    return `${this.svcurl}/api/WebViewers`;
  }
}
