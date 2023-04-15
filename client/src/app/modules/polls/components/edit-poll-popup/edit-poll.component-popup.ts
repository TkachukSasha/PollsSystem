import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {IPoll} from "../../models/poll-model";
import {PollsService} from "../../services/polls-service.service";
import {ChangePollTitle} from "../../models/change-poll-title";
import {ChangePollDescription} from "../../models/change-poll-description";
import {ChangePollDuration} from "../../models/change-poll-duration";
import {ChangePollKey} from "../../models/change-poll-key";
import {map, Observable, Subscription, tap} from "rxjs";
import {IQuestionsWithAnswers} from "../../models/question-model";
import {ApiMethod} from "../../../../core/enums/api-methods";
import {NgxSpinnerService} from "ngx-spinner";
import {ChangeQuestionText} from "../../models/change-question-text";
import {DeletePollQuestion} from "../../models/delete-poll-question";
import {HttpService} from "../../../../core/services/http/http-service";

@Component({
  selector: 'app-edit-poll-popup',
  templateUrl: './edit-poll.component-popup.html',
  styleUrls: ['./edit-poll.component-popup.scss']
})
export class EditPollComponentPopup implements OnInit {
  @Input() poll: IPoll;
  isUpdated: boolean = false;
  activeTab: string = 'General Information'
  isDisabled: boolean = false;
  isLoaded: boolean = false;
  questions: IQuestionsWithAnswers[];
  @Output()
  pollEditingRejected: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  pollEditingUpdated: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private _pollsService: PollsService,
    private _http: HttpService,
    private spinner: NgxSpinnerService
  ) {
  }

  ngOnInit(): void {
    //this.spinner.show();

    // @ts-ignore
    this._pollsService.getPollQuestions(this.poll.gid)
      .subscribe((data) => this.questions = data)

    //if(this.questions.length > 0)
      //this.onRequestExecuted();
  }

  onTabClick(name: string){
    this.activeTab = name;
  }

  onRequestExecuted(){
    this.isLoaded = true;
    this.spinner.hide();
  }

  onReject(){
    this.pollEditingRejected.emit(false);
    this.pollEditingUpdated.emit(this.isUpdated);
  }

  onTitleChange(){
    let request = new ChangePollTitle(this.poll.gid, this.poll.title);

    this._pollsService.changePollTitle(request)
      .subscribe((data) => { this.isUpdated = true; }
      )
  }

  onDescriptionChange(){
    let request = new ChangePollDescription(this.poll.gid, this.poll.description);

    this._pollsService.changePollDescription(request)
      .subscribe((data) => { this.isUpdated = true;}
      )
  }

  onDurationChange(){
    let request = new ChangePollDuration(this.poll.gid, this.poll.duration);

    this._pollsService.changePollDuration(request)
      .subscribe((data) => { this.isUpdated = true; }
      )
  }

  onKeyChange(){
    let request = new ChangePollKey(this.poll.gid, this.poll.key);

    this._pollsService.changePollKey(request)
      .subscribe((data) => { this.isUpdated = true; }
      )
  }

  onQuestionTextChange(questionGid: string, question: string){
    let request = new ChangeQuestionText(questionGid, question);

    this._pollsService.changeQuestionText(request)
      .subscribe((data) => { this.isUpdated = true; }
      )
  }

  onQuestionRemove(questionGid: string){
    let request = new DeletePollQuestion(this.poll.gid, questionGid);

    this._pollsService.deletePollQuestion(request)
      .subscribe((data) => { this.isUpdated = true; }
      )
  }

  onAnswerTextChange(questionGid: string, answerGid: string, answerName: string){

  }

  onAnswerRemove(questionGid: string, answerGid: string){

  }
}
