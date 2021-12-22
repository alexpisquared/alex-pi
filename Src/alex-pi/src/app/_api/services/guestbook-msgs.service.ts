/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { GuestbookMsg } from '../models/guestbook-msg';
@Injectable({
  providedIn: 'root',
})
class GuestbookMsgsService extends __BaseService {
  static readonly GetGuestbookMsgPath = '/api/GuestbookMsgs';
  static readonly PostGuestbookMsgPath = '/api/GuestbookMsgs';
  static readonly GetGuestbookMsg_1Path = '/api/GuestbookMsgs/{id}';
  static readonly PutGuestbookMsgPath = '/api/GuestbookMsgs/{id}';
  static readonly DeleteGuestbookMsgPath = '/api/GuestbookMsgs/{id}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @return Success
   */
  GetGuestbookMsgResponse(): __Observable<__StrictHttpResponse<Array<GuestbookMsg>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/GuestbookMsgs`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<GuestbookMsg>>;
      })
    );
  }
  /**
   * @return Success
   */
  GetGuestbookMsg(): __Observable<Array<GuestbookMsg>> {
    return this.GetGuestbookMsgResponse().pipe(
      __map(_r => _r.body as Array<GuestbookMsg>)
    );
  }

  /**
   * @param GuestbookMsg undefined
   * @return Success
   */
  PostGuestbookMsgResponse(GuestbookMsg?: GuestbookMsg): __Observable<__StrictHttpResponse<GuestbookMsg>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = GuestbookMsg;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/GuestbookMsgs`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<GuestbookMsg>;
      })
    );
  }
  /**
   * @param GuestbookMsg undefined
   * @return Success
   */
  PostGuestbookMsg(GuestbookMsg?: GuestbookMsg): __Observable<GuestbookMsg> {
    return this.PostGuestbookMsgResponse(GuestbookMsg).pipe(
      __map(_r => _r.body as GuestbookMsg)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  GetGuestbookMsg_1Response(id: number): __Observable<__StrictHttpResponse<GuestbookMsg>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/GuestbookMsgs/${id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<GuestbookMsg>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  GetGuestbookMsg_1(id: number): __Observable<GuestbookMsg> {
    return this.GetGuestbookMsg_1Response(id).pipe(
      __map(_r => _r.body as GuestbookMsg)
    );
  }

  /**
   * @param params The `GuestbookMsgsService.PutGuestbookMsgParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `GuestbookMsg`:
   */
  PutGuestbookMsgResponse(params: GuestbookMsgsService.PutGuestbookMsgParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    __body = params.GuestbookMsg;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/GuestbookMsgs/${params.id}`,
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
   * @param params The `GuestbookMsgsService.PutGuestbookMsgParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `GuestbookMsg`:
   */
  PutGuestbookMsg(params: GuestbookMsgsService.PutGuestbookMsgParams): __Observable<null> {
    return this.PutGuestbookMsgResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  DeleteGuestbookMsgResponse(id: number): __Observable<__StrictHttpResponse<GuestbookMsg>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/GuestbookMsgs/${id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<GuestbookMsg>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  DeleteGuestbookMsg(id: number): __Observable<GuestbookMsg> {
    return this.DeleteGuestbookMsgResponse(id).pipe(
      __map(_r => _r.body as GuestbookMsg)
    );
  }
}

module GuestbookMsgsService {

  /**
   * Parameters for PutGuestbookMsg
   */
  export interface PutGuestbookMsgParams {
    id: number;
    GuestbookMsg?: GuestbookMsg;
  }
}

export { GuestbookMsgsService }
