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
  }
  getNothing0(): string {
    console.log(` ▄▀ SUCCESS getNothing at ${this.svcurl}`);
    return (` ▄▀ SUCCESS getNothing at ${this.svcurl}`);
  }

  getNothing(): string {
    // @ts-ignore: error after upgrading to Angular 13.0.0
    const canvas = new OffscreenCanvas(512, 512);
    const gl1 = canvas.getContext('webgl');
    const gpr = this.getUnmaskedInfo(gl1).renderer;
    const gpv = this.getUnmaskedInfo(gl1).vendor__;
    const usa = navigator.userAgent.replace('Mozilla/5.0 (', '').replace(') AppleWebKit/537.36 (KHTML, like Gecko) Chrome/', ' ');
    const usl = usa.length - 13; // ... Safari/537.36

    //  There are several other properties you could consider to differentiate users further. Here are a few possibilities:
    // 1. **Screen Resolution**: The screen width and height (`window.screen.width` and `window.screen.height`) could be used as additional data points.
    const screenResolution = `${window.screen.width}x${window.screen.height}`;
    // 2. **Timezone**: The client's timezone (`Intl.DateTimeFormat().resolvedOptions().timeZone`) can also provide a distinguishing factor.
    const timezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    // 3. **Platform**: The platform property (`navigator.platform`) gives information about the underlying platform the browser is running on.
    const platform = navigator.platform;
    // 4. **Browser Plugin Details**: Information about plugins installed in the browser can be accessed using `navigator.plugins`.
    const browserPlugins = navigator.plugins;
    // 5. **Accept-Language Header**: The `Accept-Language` HTTP header advertises which languages the client is able to understand (`navigator.language`).
    // const acceptLanguageHeader = navigator.language;
    // 6. **Session Storage and Local Storage**: Check if these features are available in the user's browser (`window.sessionStorage` and `window.localStorage`).
    const sessionStorage = window.sessionStorage;
    // 7. **Cookies Enabled**: Check if cookies are enabled (`navigator.cookieEnabled`).
    const cookiesEnabled = navigator.cookieEnabled;
    // 8. **Color Depth**: The number of bits used to display one color (`window.screen.colorDepth`).
    const colorDepth = window.screen.colorDepth;

    const clientId = `${usa.substr(0, 27)}║${usa.substr(usl)}║CPU:${navigator.hardwareConcurrency.toString().padStart(2, '00')}║${gpr}║${window.screen.width}x${window.screen.height}║${Intl.DateTimeFormat().resolvedOptions().timeZone}║${navigator.platform}║${navigator.plugins}║${window.sessionStorage}║${navigator.cookieEnabled}║${window.screen.colorDepth}║${gpv}■■${navigator.languages}.`;

    console.log(` ▄▀ clientId: ▄▀${clientId}▄▀  `);

    return clientId;
  }
  logNothing(): string {
    try {
      this.logEvent('Nothing', 'Nothing');
      return `SUCCESS adding Nothing event to ${this.svcurl}`;
    } catch (err) {
      return (`${(err as Error).name}, ${(err as Error).message}`);
    }
  }
  logEvName(evname: string) {
    try {
      let anyString = localStorage.getItem(this.pieceOfCake);
      if (anyString === null) {
        anyString = new Date().toString();
        localStorage.setItem(this.pieceOfCake, anyString);
      }

      this.logEvent(anyString, evname);
      console.log(` ▄▀ prd mode: logged evdata to db`);
    } catch (err) {
      console.log(`${(err as Error).name}, ${(err as Error).message}`);
    }
  }
  logIfProd(evname: string): boolean {
    if (isDevMode() === false) {
      this.logEvName(evname);
      return true;
    }

    return false;
  }
  logEvent(noValue: string, evname: string) {
    const evdata = this.getNothing();

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
