/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { VwUserHopsUtc } from '../models/vw-user-hops-utc';
@Injectable({
  providedIn: 'root',
})
class VwUserHopsUtcsService extends __BaseService {
  static readonly GetVwUserHopsUtcPath = '/api/VwUserHopsUtcs';
  static readonly GetVwUserHopsUtc_1Path = '/api/VwUserHopsUtcs/{id}';
  static readonly DeleteVwUserHopsUtcPath = '/api/VwUserHopsUtcs/{id}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @return Success
   */
  GetVwUserHopsUtcResponse(): __Observable<__StrictHttpResponse<Array<VwUserHopsUtc>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/VwUserHopsUtcs`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<VwUserHopsUtc>>;
      })
    );
  }
  /**
   * @return Success
   */
  GetVwUserHopsUtc(): __Observable<Array<VwUserHopsUtc>> {
    return this.GetVwUserHopsUtcResponse().pipe(
      __map(_r => _r.body as Array<VwUserHopsUtc>)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  GetVwUserHopsUtc_1Response(id: number): __Observable<__StrictHttpResponse<VwUserHopsUtc>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/VwUserHopsUtcs/${id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<VwUserHopsUtc>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  GetVwUserHopsUtc_1(id: number): __Observable<VwUserHopsUtc> {
    return this.GetVwUserHopsUtc_1Response(id).pipe(
      __map(_r => _r.body as VwUserHopsUtc)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  DeleteVwUserHopsUtcResponse(id: number): __Observable<__StrictHttpResponse<VwUserHopsUtc>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/VwUserHopsUtcs/${id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<VwUserHopsUtc>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  DeleteVwUserHopsUtc(id: number): __Observable<VwUserHopsUtc> {
    return this.DeleteVwUserHopsUtcResponse(id).pipe(
      __map(_r => _r.body as VwUserHopsUtc)
    );
  }
}

module VwUserHopsUtcsService {
}

export { VwUserHopsUtcsService }
