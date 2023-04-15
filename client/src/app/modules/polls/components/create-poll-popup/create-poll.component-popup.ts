import {ChangeDetectionStrategy, ChangeDetectorRef, Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormArray, FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {PollsService} from "../../services/polls-service.service";
import {StorageService} from "../../../../core/services/storage/storage-service";
import {CreatePoll} from "../../models/create-poll";

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
  questions = [];
  @Output()
  pollCreatingRejected: EventEmitter<boolean> = new EventEmitter<boolean>();

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
      numOfQuestions: ['', [Validators.required]],
      duration: ['', [Validators.required]]
    })

    this.createQuestionsToPoll = this.formBuilder.group({});
  }

  onTabClick(name: string){
    this.activeTab = name;
  }

  onReject(){
    this.pollCreatingRejected.emit(false);
  }

  handlePollCreation(){
    let title = this.createPollForm.controls['title'].value;
    let description = this.createPollForm.controls['description'].value;
    this.numOfQuestions = this.createPollForm.controls['numOfQuestions'].value;
    let duration = this.createPollForm.controls['duration'].value;

    // @ts-ignore
    let data = JSON.parse(this._storage.getData('auth'));

    let request = new CreatePoll(title, description, this.numOfQuestions, duration, data?.userGid);

    this._pollsService.createPoll(request)
      .subscribe((data) => {
        if(data.status){
          this.pollGid = data.pollGid;
          for (let i = 0; i < this.numOfQuestions; i++) {
            const questionId = Date.now().toString();
            const question = this.formBuilder.group({
              text: ['', Validators.required],
              answers: this.formBuilder.array([]) // Define answers as a FormArray
            });
            this.createQuestionsToPoll.addControl(questionId, question);
          }
          this.onTabClick('Questions');
          this.cdr.detectChanges();
        }
      })
  }

  onAnswerAdd(questionId: string){
    const answers = this.createQuestionsToPoll.get(questionId + '.answers') as FormArray;
    answers.push(this.formBuilder.group({
      text: ''
    }));
  }

  onAnswerRemove(questionId: string, index: number) {
    const answers = this.createQuestionsToPoll.get(questionId + '.answers') as FormArray;
    answers.removeAt(index);
  }
}
