import {ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {PollsService} from "../../services/polls-service.service";
import {StorageService} from "../../../../core/services/storage/storage-service";
import {CreatePoll} from "../../models/create-poll";
import {IScore} from "../../models/score";
import {CreatePollQuestions} from "../../models/create-poll-questions";

@Component({
  selector: 'app-create-poll-popup',
  templateUrl: './create-poll.component-popup.html',
  styleUrls: ['./create-poll.component-popup.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreatePollComponentPopup implements OnInit {
  pollGid: string;
  activeTab: string = 'General Information';
  numOfQuestions: number;
  createPollForm!: FormGroup;
  createQuestionsToPoll!: FormGroup;
  scores: IScore[];
  @Output()
  pollCreatingRejected: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output()
  pollQuestionsCreated: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(
    private formBuilder: FormBuilder,
    private _storage: StorageService,
    private _pollsService: PollsService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.createPollForm = this.formBuilder.group({
      title: ['', [Validators.required, Validators.minLength(4)]],
      description: ['', [Validators.required, Validators.minLength(4)]],
      numOfQuestions: [null, [Validators.required]],
      duration: [null, [Validators.required]]
    })

    this.createQuestionsToPoll = this.formBuilder.group({
      questions: this.formBuilder.array([])
    });

    this._pollsService.getScores()
      .subscribe((data) => this.scores = data);
  }

  questions(): FormArray {
    return this.createQuestionsToPoll.get('questions') as FormArray;
  }

  newQuestion(): FormGroup {
    return this.formBuilder.group({
      questionText: '',
      answers: this.formBuilder.array([])
    });
  }

  addQuestion() {
    this.questions().push(this.newQuestion());

    this.cdr.detectChanges();
  }

  questionAnswers(questionIndex: number): FormArray {
    return this.questions()
      .at(questionIndex)
      .get('answers') as FormArray;
  }

  newAnswer(): FormGroup {
    return this.formBuilder.group({
      answerText: '',
      scoreGid: ''
    });
  }

  addQuestionAnswer(questionIndex: number) {
    this.questionAnswers(questionIndex).push(this.newAnswer());
    this.cdr.detectChanges();
  }

  removeQuestionAnswer(questionIndex: number, answerIndex: number) {
    this.questionAnswers(questionIndex).removeAt(answerIndex);
    this.cdr.detectChanges();
  }

  onTabClick(name: string){
    this.activeTab = name;
  }

  onReject(){
    this.pollCreatingRejected.emit(false);
  }

  onCreated(){
    this.pollQuestionsCreated.emit(true);
  }

  handlePollCreation(){
    let title = this.createPollForm.controls['title'].value;
    let description = this.createPollForm.controls['description'].value;
    this.numOfQuestions = Number(this.createPollForm.controls['numOfQuestions'].value);
    let duration = Number(this.createPollForm.controls['duration'].value);

    // @ts-ignore
    let data = JSON.parse(this._storage.getData('auth'));

    let request = new CreatePoll(title, description, this.numOfQuestions, duration, data?.userGid);

    this._pollsService.createPoll(request)
      .subscribe((data) => {
        if(data.status){
          this.pollGid = data.pollGid;
          for (let i = 0; i < this.numOfQuestions; i++) {
            this.addQuestion();
          }
          this.onTabClick('Questions');
          this.cdr.detectChanges();
        }
      })
  }

  onSubmit(){
    let request = new CreatePollQuestions(this.pollGid, this.createQuestionsToPoll.value.questions);

    console.log(`request: ${JSON.stringify(request)}`)

    this._pollsService.createPollQuestions(request)
      .subscribe((data) => {
        if(data)
          console.log(data)
          this.onCreated();
          location.reload();
      })
  }
}
