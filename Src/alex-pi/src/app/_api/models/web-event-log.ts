/* tslint:disable */
import { WebsiteUser } from './website-user';
export interface WebEventLog {
  id?: number;
  websiteUserId?: number;
  eventName?: string;
  doneAt?: string;
  websiteUser?: WebsiteUser;
  eventData__Copy?: string;
}
