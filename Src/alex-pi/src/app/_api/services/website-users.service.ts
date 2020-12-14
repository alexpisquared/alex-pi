/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { WebsiteUser } from '../models/website-user';
@Injectable({
  providedIn: 'root',
})
class WebsiteUsersService extends __BaseService {
  static readonly GetWebsiteUserPath = '/api/WebsiteUsers';
  static readonly PostWebsiteUserPath = '/api/WebsiteUsers';
  static readonly GetWebsiteUser_1Path = '/api/WebsiteUsers/{id}';
  static readonly PutWebsiteUserPath = '/api/WebsiteUsers/{id}';
  static readonly DeleteWebsiteUserPath = '/api/WebsiteUsers/{id}';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @return Success
   */
  GetWebsiteUserResponse(): __Observable<__StrictHttpResponse<Array<WebsiteUser>>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/WebsiteUsers`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<Array<WebsiteUser>>;
      })
    );
  }
  /**
   * @return Success
   */
  GetWebsiteUser(): __Observable<Array<WebsiteUser>> {
    return this.GetWebsiteUserResponse().pipe(
      __map(_r => _r.body as Array<WebsiteUser>)
    );
  }

  /**
   * @param websiteUser undefined
   * @return Success
   */
  PostWebsiteUserResponse(websiteUser?: WebsiteUser): __Observable<__StrictHttpResponse<WebsiteUser>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = websiteUser;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/WebsiteUsers`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<WebsiteUser>;
      })
    );
  }
  /**
   * @param websiteUser undefined
   * @return Success
   */
  PostWebsiteUser(websiteUser?: WebsiteUser): __Observable<WebsiteUser> {
    return this.PostWebsiteUserResponse(websiteUser).pipe(
      __map(_r => _r.body as WebsiteUser)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  GetWebsiteUser_1Response(id: number): __Observable<__StrictHttpResponse<WebsiteUser>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/WebsiteUsers/${id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<WebsiteUser>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  GetWebsiteUser_1(id: number): __Observable<WebsiteUser> {
    return this.GetWebsiteUser_1Response(id).pipe(
      __map(_r => _r.body as WebsiteUser)
    );
  }

  /**
   * @param params The `WebsiteUsersService.PutWebsiteUserParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `websiteUser`:
   */
  PutWebsiteUserResponse(params: WebsiteUsersService.PutWebsiteUserParams): __Observable<__StrictHttpResponse<null>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    __body = params.websiteUser;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/WebsiteUsers/${params.id}`,
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
   * @param params The `WebsiteUsersService.PutWebsiteUserParams` containing the following parameters:
   *
   * - `id`:
   *
   * - `websiteUser`:
   */
  PutWebsiteUser(params: WebsiteUsersService.PutWebsiteUserParams): __Observable<null> {
    return this.PutWebsiteUserResponse(params).pipe(
      __map(_r => _r.body as null)
    );
  }

  /**
   * @param id undefined
   * @return Success
   */
  DeleteWebsiteUserResponse(id: number): __Observable<__StrictHttpResponse<WebsiteUser>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;

    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/WebsiteUsers/${id}`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<WebsiteUser>;
      })
    );
  }
  /**
   * @param id undefined
   * @return Success
   */
  DeleteWebsiteUser(id: number): __Observable<WebsiteUser> {
    return this.DeleteWebsiteUserResponse(id).pipe(
      __map(_r => _r.body as WebsiteUser)
    );
  }
}

module WebsiteUsersService {

  /**
   * Parameters for PutWebsiteUser
   */
  export interface PutWebsiteUserParams {
    id: number;
    websiteUser?: WebsiteUser;
  }
}

export { WebsiteUsersService }
