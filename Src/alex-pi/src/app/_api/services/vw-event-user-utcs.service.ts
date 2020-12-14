/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { VwEventUserUtc } from '../models/vw-event-user-utc';
@Injectable({
  providedIn: 'root',
})
class VwEventUserUtcsService extends __BaseService {
  static readonly GetVwEventUserUtcPath = '/api/VwEventUserUtcs';
  static readonly GetJustAPOCPath = '/api/VwEventUserUtcs/{a}/{b}/{c}';
  static readonly GetVwEventUserUtcWithParamPath = '/api/VwEventUserUtcs/{nickname}/{userId}';
  static readonly GetVwEventUserUtc_1Path = '/api/VwEventUserUtcs/{DoneAt}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @return Success
   */
  GetVwEventUserUtcResponse(): __Observable<__StrictHttpResponse<Array<VwEventUserUtc>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/VwEventUserUtcs`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<VwEventUserUtc>>;
      })
    );
  }
  /**
   * @return Success
   */
  GetVwEventUserUtc(): __Observable<Array<VwEventUserUtc>> {
    return this.GetVwEventUserUtcResponse().pipe(
      __map(_r => _r.body as Array<VwEventUserUtc>)
    );
  }

  /**
   * @param params The `VwEventUserUtcsService.GetJustAPOCParams` containing the following parameters:
   *
   * - `c`:
   *
   * - `b`:
   *
   * - `a`:
   *
   * @return Success
   */
  GetJustAPOCResponse(params: VwEventUserUtcsService.GetJustAPOCParams): __Observable<__StrictHttpResponse<string>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;



    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/VwEventUserUtcs/${params.a}/${params.b}/${params.c}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'text'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<string>;
      })
    );
  }
  /**
   * @param params The `VwEventUserUtcsService.GetJustAPOCParams` containing the following parameters:
   *
   * - `c`:
   *
   * - `b`:
   *
   * - `a`:
   *
   * @return Success
   */
  GetJustAPOC(params: VwEventUserUtcsService.GetJustAPOCParams): __Observable<string> {
    return this.GetJustAPOCResponse(params).pipe(
      __map(_r => _r.body as string)
    );
  }

  /**
   * @param params The `VwEventUserUtcsService.GetVwEventUserUtcWithParamParams` containing the following parameters:
   *
   * - `userId`:
   *
   * - `nickname`:
   *
   * @return Success
   */
  GetVwEventUserUtcWithParamResponse(params: VwEventUserUtcsService.GetVwEventUserUtcWithParamParams): __Observable<__StrictHttpResponse<Array<VwEventUserUtc>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;


    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/VwEventUserUtcs/${params.nickname}/${params.userId}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<VwEventUserUtc>>;
      })
    );
  }
  /**
   * @param params The `VwEventUserUtcsService.GetVwEventUserUtcWithParamParams` containing the following parameters:
   *
   * - `userId`:
   *
   * - `nickname`:
   *
   * @return Success
   */
  GetVwEventUserUtcWithParam(params: VwEventUserUtcsService.GetVwEventUserUtcWithParamParams): __Observable<Array<VwEventUserUtc>> {
    return this.GetVwEventUserUtcWithParamResponse(params).pipe(
      __map(_r => _r.body as Array<VwEventUserUtc>)
    );
  }

  /**
   * @param DoneAt undefined
   * @return Success
   */
  GetVwEventUserUtc_1Response(DoneAt: string): __Observable<__StrictHttpResponse<VwEventUserUtc>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/VwEventUserUtcs/${DoneAt}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<VwEventUserUtc>;
      })
    );
  }
  /**
   * @param DoneAt undefined
   * @return Success
   */
  GetVwEventUserUtc_1(DoneAt: string): __Observable<VwEventUserUtc> {
    return this.GetVwEventUserUtc_1Response(DoneAt).pipe(
      __map(_r => _r.body as VwEventUserUtc)
    );
  }
}

module VwEventUserUtcsService {

  /**
   * Parameters for GetJustAPOC
   */
  export interface GetJustAPOCParams {
    c: number;
    b: number;
    a: number;
  }

  /**
   * Parameters for GetVwEventUserUtcWithParam
   */
  export interface GetVwEventUserUtcWithParamParams {
    userId: number;
    nickname: string;
  }
}

export { VwEventUserUtcsService }
