import {Component, EventEmitter, Input, OnDestroy, OnInit, Output} from '@angular/core';
import {IPoll} from "../../models/poll-model";
import {HttpService} from "../../../../core/services/http/http-service";
import {NgxSpinnerService} from "ngx-spinner";
import {map, Subscription, tap} from "rxjs";
import { writeFile } from 'xlsx';
import * as XLSX from 'xlsx';
import {ApiMethod} from "../../../../core/enums/api-methods";
import {IBaseCalculations, ICalculations} from "../../models/poll-calculations";

@Component({
  selector: 'app-excel-selector-popup',
  templateUrl: './excel-selector-popup.component.html',
  styleUrls: ['./excel-selector-popup.component.scss']
})
export class ExcelSelectorPopupComponent implements OnInit, OnDestroy {
  @Input() poll: IPoll;
  data: any = [];
  @Output()
  pollExportRejected: EventEmitter<boolean> = new EventEmitter<boolean>();
  resultsSubscription: Subscription;

  calculations = [
    { id: 0, name: 'Base', disabled: false },
    { id: 1, name: 'Quartiles', disabled: false },
    { id: 2, name: 'Percentiles', disabled: false },
    { id: 3, name: 'Distributions', disabled: false },
    { id: 4, name: 'Populations', disabled: false },
    { id: 5, name: 'Additional', disabled: false },
    { id: 6, name: 'Full', disabled: false },
  ];

  selectedCalculationId: number;

  constructor(
    private _http: HttpService,
    private _spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.resultsSubscription.unsubscribe();
  }

  onCheckboxChange(event) {
    const calculationId = event.target.value;

    if (event.target.checked) {
      this.selectedCalculationId = calculationId;

      // disable other checkboxes
      this.calculations.forEach(calculation => {
        if (calculation.id !== this.selectedCalculationId) {
          calculation.disabled = true;
        }
      });
    } else {
      this.selectedCalculationId = null;

      // enable all checkboxes
      this.calculations.forEach(calculation => {
        calculation.disabled = false;
      });
    }
  }

  onExport(){
    this._spinner.show();

    // @ts-ignore
    this.resultsSubscription = this._http.requestCall<ICalculations>(`/statistics/calculations?Type=${this.selectedCalculationId}&PollGid=${this.poll.gid}`, ApiMethod.GET)
      .pipe(
        map((data) => {
          this.onTypeExport(this.selectedCalculationId, data);
        }),
        tap(() => this.onRequestExecuted())
      )
      .subscribe(
        () => {
        },
        (error) => console.error(error)
      );
  }

  onTypeExport(selectedCalculationId: number, data?: any){
    if(this.selectedCalculationId == 6){
      console.log('test-type')
      const worksheet = XLSX.utils.json_to_sheet([data.baseCalculations, data.additional, data.distributions, data.percentiles, data.populations, data.quartiles]);
      const workbook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');
      writeFile(workbook, 'calculations.xlsx')
    }
    else{
      console.log('test-else')
      this.data.push(data);
      const worksheet = XLSX.utils.json_to_sheet(this.data);
      const workbook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(workbook, worksheet, 'Sheet1');
      writeFile(workbook, 'calculations.xlsx')
    }
  }

  onRequestExecuted(){
    this._spinner.hide();
  }

  onReject(){
    this.pollExportRejected.emit(false);
  }
}
