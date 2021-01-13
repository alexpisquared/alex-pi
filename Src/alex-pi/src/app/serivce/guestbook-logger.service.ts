import { HttpClient } from '@angular/common/http';
import { Injectable, isDevMode } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GuestbookLoggerService {

  private svcurl = isDevMode() ? 'https://localhost:5001' : 'https://alex-pi-api.azurewebsites.net';

  constructor(private http: HttpClient) { }

}
