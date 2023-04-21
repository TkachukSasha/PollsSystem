import {ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {IPoll} from "../../models/poll-model";
import {PollsService} from "../../services/polls-service.service";
import {ChangePollTitle} from "../../models/change-poll-title";
import {ChangePollDescription} from "../../models/change-poll-description";
import {ChangePollDuration} from "../../models/change-poll-duration";
import {ChangePollKey} from "../../models/change-poll-key";
import {IQuestionsWithAnswersAndScores} from "../../models/question-model";
import {NgxSpinnerService} from "ngx-spinner";
import {ChangeQuestionText} from "../../models/change-question-text";
import {DeletePollQuestion} from "../../models/delete-poll-question";
import {HttpService} from "../../../../core/services/http/http-service";
import {ChangeAnswerText} from "../../models/change-answer-text";
import {DeleteQuestionAnswer} from "../../models/delete-question-answer";
import {IScore} from "../../models/score";
import {ChangeAnswerScore} from "../../models/change-answer-score";

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
  questions: IQuestionsWithAnswersAndScores[];
  selectedAnswerScoreGids = {};
  scores: IScore[];
  @Output()
  pollEditingRejected: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  pollEditingUpdated: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private _pollsService: PollsService,
    private _http: HttpService,
    private spinner: NgxSpinnerService,
    private cdr: ChangeDetectorRef
  ) {
  }

  ngOnInit(): void {
    this.spinner.show();

    // @ts-ignore
    this._pollsService.getPollQuestionsWithScores(this.poll.gid)
      .subscribe((data) => {
        this.questions = data;

        for (const question of this.questions) {
          for (const answer of question.answers) {
            this.selectedAnswerScoreGids[answer.gid] = answer.scoreGid;
          }
        }
      });

    this._pollsService.getScores()
      .subscribe((data) => this.scores = data);

    this.onRequestExecuted();
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
      .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
      )

    this.cdr.detectChanges();
  }

  onDescriptionChange(){
    let request = new ChangePollDescription(this.poll.gid, this.poll.description);

    this._pollsService.changePollDescription(request)
      .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
      )

    this.cdr.detectChanges();
  }

  onDurationChange(){
    let request = new ChangePollDuration(this.poll.gid, this.poll.duration);

    this._pollsService.changePollDuration(request)
      .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
      )

    this.cdr.detectChanges();
  }

  onKeyChange(){
    let request = new ChangePollKey(this.poll.gid, this.poll.key);

    this._pollsService.changePollKey(request)
      .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
      )

    this.cdr.detectChanges();
  }

  onQuestionTextChange(questionGid: string, question: string){
    let request = new ChangeQuestionText(questionGid, question);

    this._pollsService.changeQuestionText(request)
      .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
      )

    this.cdr.detectChanges();
  }

  onQuestionRemove(questionGid: string){
    let request = new DeletePollQuestion(this.poll.gid, questionGid);

    this._pollsService.deletePollQuestion(request)
      .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
      )

    this.cdr.detectChanges();
  }

  onAnswerTextChange(questionGid: string, answerGid: string, answerName: string, answerScoreGid, selectedScoreGid: string){
    let request = new ChangeAnswerText(questionGid, answerGid, answerName);

    this._pollsService.changeAnswerText(request)
      .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
      )

    if(answerScoreGid !== selectedScoreGid)
    {
      let request = new ChangeAnswerScore(questionGid, answerGid, selectedScoreGid);

      this._pollsService.changeAnswerScore(request)
        .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
        )
    }

    this.cdr.detectChanges();
  }

  onAnswerRemove(questionGid: string, answerGid: string){
   let request = new DeleteQuestionAnswer(questionGid, answerGid);

   this._pollsService.deleteQuestionAnswer(request)
     .subscribe((data) => { data ? this.isUpdated = true :  this.isUpdated = false }
     )

    this.cdr.detectChanges();
  }
}
