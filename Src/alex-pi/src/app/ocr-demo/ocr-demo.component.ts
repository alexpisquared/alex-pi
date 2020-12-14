import { Component, OnInit, ViewChild, ElementRef, AfterViewInit, isDevMode } from '@angular/core';
import { WebEventLoggerService } from '../serivce/web-event-logger.service';
import { OcrService } from '../_api/services';
import { fromEvent } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-ocr-demo',
  templateUrl: './ocr-demo.component.html',
  styleUrls: ['./ocr-demo.component.scss']
})
export class OcrDemoComponent implements OnInit, AfterViewInit {
  @ViewChild('urlInput') urlInput: ElementRef;
  @ViewChild('myCanvas', { static: true }) myCanvas: ElementRef;
  @ViewChild('myImage', { static: true }) myImage: ElementRef;
  dataavailbale = false;
  report = 'All Clear!';
  imgurlA = 'https://github.com/Azure-Samples/cognitive-services-sample-data-files/raw/master/ComputerVision/Images/printed_text.jpg';
  imgurl0 = 'https://alex-pi.azurewebsites.net/assets/images/ocr1.jpg';
  imgurl1 = 'https://alex-pi.azurewebsites.net/assets/images/ocr2.jpg';
  imgurl2 = 'https://alex-pi.azurewebsites.net/assets/images/ocr3.jpg';
  imgurl = './assets/images/transparent5x5.jpg';
  result: string;
  errmsg: string;
  cctx: CanvasRenderingContext2D;
  hratio = 0.2247;
  showBigImg = false;

  constructor(private welSvc: WebEventLoggerService, private ocrsvc: OcrService) {}

  ngOnInit() {
    this.welSvc.logIfProd('ocr-');
    this.cctx = this.myCanvas.nativeElement.getContext('2d');

    if (isDevMode()) {
      // this.imgurl = this.imgurl0;
      // // this.startOcr();
      // // // this.paint(this._ctx);
    }
  }
  ngAfterViewInit() {
    fromEvent(this.myImage.nativeElement, 'load').subscribe(val => this.calcRatio('from event'));

    fromEvent(this.urlInput.nativeElement, 'keyup')
      .pipe(
        debounceTime(250),
        distinctUntilChanged()
      )
      .subscribe(val => (this.imgurl = this.urlInput.nativeElement.value));
  }

  paint(ctx) {
    // ctx.scale(1, 4); // Doubles size of anything draw to canvas.

    ctx.clearRect(0, 0, this.myCanvas.nativeElement.width, this.myCanvas.nativeElement.height);
    ctx.strokeStyle = '#0f0';
    ctx.fillStyle = '#0f0';
    ctx.lineWidth = 0.5;
    ctx.font = 'italic 12pt Calibri';
  }
  private calcRatio(msg: string) {
    console.log(` ## ${msg} ==> `);
    if (this.myImage.nativeElement.naturalHeight) {
      this.hratio = this.myImage.nativeElement.offsetHeight / this.myImage.nativeElement.naturalHeight;

      // tu: https://stackoverflow.com/questions/4938346/canvas-width-and-height-in-html5 ---
      // Make a canvas that has a blurry pixelated zoom-in  with each canvas pixel drawn showing as roughly 2x2 on screen canvas.width = 400; canvas.height = 300; canvas.style.width = '800px'; canvas.style.height = '600px';
      this.myCanvas.nativeElement.width = this.myImage.nativeElement.naturalWidth * this.hratio;

      console.log(` ## ${this.hratio}  AA  <= ${this.myImage.nativeElement.offsetWidth}x${this.myImage.nativeElement.offsetHeight} / ${this.myImage.nativeElement.naturalWidth}x${this.myImage.nativeElement.naturalHeight}`);
    } else {
      console.log(` ## NOT GOOD !!! ${this.myImage.nativeElement.naturalWidth}x${this.myImage.nativeElement.naturalHeight} @@@@@@@@@@@@@`);
    }
  }

  onImageButtonClick(imgurl: string) {
    this.showBigImg = true;
    this.urlInput.nativeElement.value = this.imgurl = imgurl;
    this.myImage.nativeElement.src = imgurl;
    this.result = '';
    this.cctx.clearRect(0, 0, this.myCanvas.nativeElement.width, this.myCanvas.nativeElement.height);
  }

  onImageButtonClick2(imgurl: string) {
    // http://diveintohtml5.info/examples/halma.js
    const kBoardWidth = 9;
    const kBoardHeight = 9;
    const kPieceWidth = 50;
    const kPieceHeight = 50;
    const kPixelWidth = 1 + kBoardWidth * kPieceWidth;
    const kPixelHeight = 1 + kBoardHeight * kPieceHeight;
    const radius = kPieceWidth / 2 - kPieceWidth / 10;

    // this._ctx.scale(1, 1); // Doubles size of anything draw to canvas.

    this.cctx.beginPath();
    /* vertical lines */
    for (let x = 0; x <= kPixelWidth; x += kPieceWidth) {
      this.cctx.moveTo(0.5 + x, 0);
      this.cctx.lineTo(0.5 + x, kPixelHeight);
    }

    /* horizontal lines */
    for (let y = 0; y <= kPixelHeight; y += kPieceHeight) {
      this.cctx.moveTo(0, 0.5 + y);
      this.cctx.lineTo(kPixelWidth, 0.5 + y);
    }

    /* draw it! */
    this.cctx.strokeStyle = '#f00';
    this.cctx.stroke();
    // this._ctx.arc(x, y, radius, 0, Math.PI * 2, false);
    this.cctx.closePath();
  }
  startOcr() {
    this.welSvc.logIfProd('ocr!');

    const urlEscaped = btoa(this.imgurl); // slash issue fixed by double encoding
    // console.log('@@ url:' + urlEscaped);

    this.result = this.errmsg = 'Wait! Working on it...';

    this.ocrsvc.Get(urlEscaped).subscribe(
      data => {
        this.result = this.errmsg = data.replace('url:', 'url:<br /><br />'); // pessimistic mode: in case of error insert \r\n.

        this.paint(this.cctx);

        const response = JSON.parse(data);
        this.result = response.regions.length > 0 ? this.extractTextFromResponse(response) : data;

        // console.log('@@ 2 - ' + this.result);
        if (this.result.length > 0) {
          this.dataavailbale = true;
          this.errmsg = '';
        } else {
          this.dataavailbale = false;
          this.errmsg = 'zero length result; go figure...';
        }
      },
      err => {
        this.errmsg = err.message
          // .toString()
          // .replace('https', '<br /><br />https')
          // .replace(': ', '<br /><br />')
          ;
        console.log(`@@ err: ${err.message}`);
      }
    );
  }
  extractTextFromResponse = response => {
    let text = '';
    text += '<br />';
    console.log(` ## ${this.hratio}  BB    ${response.textAngle}`);

    this.cctx.resetTransform();
    this.cctx.translate(this.myCanvas.nativeElement.width / 2, this.myCanvas.nativeElement.height / 2);
    this.cctx.rotate(-response.textAngle);
    this.cctx.translate(-this.myCanvas.nativeElement.width / 2, -this.myCanvas.nativeElement.height / 2);

    response.regions.forEach(region => {
      region.lines.forEach(line => {
        line.words.forEach(word => {
          text += word.text + ' ';
          const bb = word.boundingBox.split(',');
          const x = bb[0] * this.hratio;
          const y = bb[1] * this.hratio;
          this.cctx.strokeRect(x, y, bb[2] * this.hratio, bb[3] * this.hratio);
          this.cctx.fillText(`${word.text}`, x, y);
        });
        text += '<br />';
      });
      text += '<br />';
    });
    return text;
  };
  deleteconfirmation(id: string) {
    if (confirm('Are you sure you want to delete this ?')) {
      this.welSvc.deleteWebEventLog(id).subscribe(res => {
        alert('Deleted successfully !!!');
        this.startOcr();
      });
    }
  }
  clearSearchInput() {
    this.urlInput.nativeElement.value = '';
    const event = new KeyboardEvent('keyup', { bubbles: true });
    this.urlInput.nativeElement.dispatchEvent(event);
  }
  startOcrOld() {
    const urlEscaped = encodeURIComponent(this.imgurl);
    console.log('@@ url:' + urlEscaped);
    this.welSvc.getOcr2(urlEscaped).subscribe(
      data => {
        this.result = '@@ 2 - ' + data;
        console.log('@@ 2 - ' + this.result);
        if (this.result.length > 0) {
          this.dataavailbale = true;
        } else {
          this.dataavailbale = false;
        }
      },
      err => {
        this.errmsg = err;
        console.log(err);
        console.log(`@@ err: ${err.message}`);
      }
    );
  }
  startOcrSimpleTest() {
    this.welSvc.getOcr0().subscribe(
      data => {
        this.result = '@@ 1 - ' + data;
        console.log('@@ 1 - ' + this.result);
        if (this.result.length > 0) {
          this.dataavailbale = true;
        } else {
          this.dataavailbale = false;
        }
      },
      err => {
        this.errmsg = err.message;
        console.log(`@@ err: ${err.message}`);
      }
    );
  }
}
