import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-time-bar',
  templateUrl: './time-bar.component.html',
  styleUrls: ['./time-bar.component.scss']
})
export class TimeBarComponent implements OnInit, OnDestroy {
  timeRemaining: string = '0:00';
  intervalId: any;
  @Input() duration: number;
  @Output() timeEnded = new EventEmitter<void>();

  constructor() {
  }

  ngOnInit() {
    let remaining = this.duration;

    this.intervalId = setInterval(() => {
      remaining--;
      this.timeRemaining = this.formatTime(remaining);

      if (remaining <= 0) {
        clearInterval(this.intervalId);
        this.timeRemaining = '0:00';
        this.timeEnded.emit();
      }
      else{
        // @ts-ignore
        document.getElementById("progress-text").innerHTML = this.timeRemaining + "";
      }
    }, 1000);
  }

  formatTime(seconds: number): string {
    const minutes = Math.floor(seconds / 60);
    const remainingSeconds = seconds % 60;
    return `${minutes}:${remainingSeconds < 10 ? '0' : ''}${remainingSeconds}`;
  }

  ngOnDestroy(): void {
    clearInterval(this.intervalId);
  }
}
