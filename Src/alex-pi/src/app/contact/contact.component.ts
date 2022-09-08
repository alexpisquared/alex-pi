import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { WebEventLoggerService } from '../serivce/web-event-logger.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  messageForm: UntypedFormGroup;
  submitted = false;
  success = false;
  biglink = 'mailto: alex.pigida@outlook.com?cc=pigida@gmail.com&subject=reaching you from alexPi.ca&body=Hi Alex,';

  constructor(private formBuilder: UntypedFormBuilder, private welSvc: WebEventLoggerService) { }

  ngOnInit() {
    this.welSvc.logIfProd('cntc');
    this.messageForm = this.formBuilder.group({
      // sender: ['', Validators.required],
      textareaMsg: ['', Validators.required]
    });
  }

  onSubmit() {
    this.submitted = true;
    if (this.messageForm.invalid) {
      return;
    }

    this.biglink = `mailto: alex.pigida@outlook.com?cc=pigida@gmail.com&subject=reaching you from alexPi.ca ... &body=${this.messageForm.controls.textareaMsg.value}`;

    this.success = true;
  }
}
