/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { WebEventLog } from '../models/web-event-log';
@Injectable({
  providedIn: 'root',
})
class WebEventLogsService extends __BaseService {
  static readonly GetWebEventLogPath = '/api/WebEventLogs';
  static readonly PostWebEventLogPath = '/api/WebEventLogs';
  static readonly GetWebEventLog_1Path = '/api/WebEventLogs/{id}';
  static readonly PutWebEventLogPath = '/api/WebEventLogs/{id}';
  static readonly DeleteWebEventLogPath = '/api/WebEventLogs/{id}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @return Success
   */
  GetWebEventLogResponse(): __Observable<__StrictHttpResponse<Array<WebEventLog>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/WebEventLogs`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<WebEventLog>>;
      })
    );
  }
  /**
   * @return Success
   */
  GetWebEventLog(): __Observable<Array<WebEventLog>> {
    return this.GetWebEventLogResponse().pipe(
      __map(_r => _r.body as Array<WebEventLog>)
    );
  }

  /**
   * @param webEventLog undefined
   * @return Success
   */
  PostWebEventLogResponse(webEventLog?: WebEventLog): __Observable<__StrictHttpResponse<WebEventLog>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = webEventLog;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/WebEventLogs`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<WebEventLog>;
      })
    );
  }
  /**
   * @param webEventLog undefined
   * @return Success
   */
  PostWebEventLog(webEventLog?: WebEventLog): __Observable<WebEventLog> {
    return this.PostWebEventLogResponse(webEventLog).pipe(
      __map(_r => _r.body as WebEventLog)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  GetWebEventLog_1Response(id: number): __Observable<__StrictHttpResponse<WebEventLog>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/WebEventLogs/${id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<WebEventLog>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  GetWebEventLog_1(id: number): __Observable<WebEventLog> {
    return this.GetWebEventLog_1Response(id).pipe(
      __map(_r => _r.body as WebEventLog)
    );
  }

  /**
   * @param params The `WebEventLogsService.PutWebEventLogParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `webEventLog`:
   */
  PutWebEventLogResponse(params: WebEventLogsService.PutWebEventLogParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    __body = params.webEventLog;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/WebEventLogs/${params.id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<null>;
      })
    );
  }
  /**
   * @param params The `WebEventLogsService.PutWebEventLogParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `webEventLog`:
   */
  PutWebEventLog(params: WebEventLogsService.PutWebEventLogParams): __Observable<null> {
    return this.PutWebEventLogResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  DeleteWebEventLogResponse(id: number): __Observable<__StrictHttpResponse<WebEventLog>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/WebEventLogs/${id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<WebEventLog>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  DeleteWebEventLog(id: number): __Observable<WebEventLog> {
    return this.DeleteWebEventLogResponse(id).pipe(
      __map(_r => _r.body as WebEventLog)
    );
  }
}

module WebEventLogsService {

  /**
   * Parameters for PutWebEventLog
   */
  export interface PutWebEventLogParams {
    id: number;
    webEventLog?: WebEventLog;
  }
}

export { WebEventLogsService }
