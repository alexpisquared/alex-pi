/* tslint:disable */
import { WebEventLog } from './web-event-log';
export interface WebsiteUser {
  reviewedBy?: number;
  id?: number;
  nickname?: string;
  note?: string;
  doNotLog?: boolean;
  eventData?: string;
  reviewedAt?: string;
  lastVisitAt?: string;
  createdAt?: string;
  webEventLog?: Array<WebEventLog>;
  visitCount__New?: number;
}
