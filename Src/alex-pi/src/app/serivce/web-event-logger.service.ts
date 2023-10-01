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
  private svcurl = isDevMode() ? 'https://localhost:5001' : 'https://alex-pi-api.azurewebsites.net';

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
  getClientInfo(): string {
    // @ts-ignore: error after upgrading to Angular 13.0.0
    const canvas = new OffscreenCanvas(512, 512);
    const gl1 = canvas.getContext('webgl');
    const gpr = this.getUnmaskedInfo(gl1).renderer;
    const gpv = this.getUnmaskedInfo(gl1).vendor__;
    const usa = navigator.userAgent.replace('Mozilla/5.0 (', '').replace(') AppleWebKit/537.36 (KHTML, like Gecko) Chrome/', ' ');
    const usl = usa.length - 13; // ... Safari/537.36

    const clientId = `${usa.substr(0, 27)}**${usa.substr(usl)}**${navigator.languages}**CPU:${navigator.hardwareConcurrency}**${gpr}**${gpv}.`;

    console.log(` ▄▀ clientId:  "${clientId}"`);

    return clientId;
  }
  logIfProd(evname: string) {
    if (isDevMode() === false) {
      this.logEvent(evname);
      console.log(` ▄▀ prd mode: logged evdata to db`);
    } else {
      const evdata = this.getClientInfo();
      console.log(` ▄▀ dev mode: no logging of  "${evdata}"`);
    }
  }
  logEvent(evname: string) {
    const evdata = this.getClientInfo();

    this.addEvent(evname, evdata).subscribe(
      data => {
        console.log(' ▄▀ SUCCESS adding event to db:  ');
        console.log(data);
      },
      err => {
        console.log(` ▄▀ ERR in logEvent(${evname}):  ${err.message}`);
      }
    );
  }

  addEvent(evname: string, evdata: string): Observable<WebEventLog> {
    const wel = new WebEventLog();
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
