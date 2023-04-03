import { ChangeDetectionStrategy, Component, Input, OnDestroy, OnInit } from '@angular/core';
import { IPoll } from "../../models/poll-model";
import { PollsService } from "../../services/polls-service.service";
import { CheckPollKey } from "../../models/check-poll-key";
import { Router } from "@angular/router";
import { Subscription } from "rxjs";

@Component({
  selector: 'app-poll-item-popup',
  templateUrl: './poll-item-popup.component.html',
  styleUrls: ['./poll-item-popup.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PollItemPopupComponent implements OnInit, OnDestroy {
  @Input() poll: IPoll
  isKeyVerified: boolean = false;
  pollKey: string = '';
  private pollItemSubscription: Subscription;

  constructor(
    private router: Router,
    private _pollService: PollsService
  ) { }

  ngOnInit(): void {
  }

  ngOnDestroy(): void {
    if (this.pollItemSubscription) {
      this.pollItemSubscription.unsubscribe();
    }
  }

  onPollSubmit(){
    let request = new CheckPollKey(this.poll.gid, this.pollKey);

    this.pollItemSubscription = this._pollService.checkPollKey(request)
      .subscribe(
        (data) => {
          this.isKeyVerified = data;

          if(this.isKeyVerified === true){
            this.router.navigate(['/polls/poll-pass/', this.poll.gid, this.poll.duration]);
          }
        }
      )
  }

}
