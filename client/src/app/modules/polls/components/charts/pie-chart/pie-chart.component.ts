import {Component, Input, OnInit} from '@angular/core';
import {IPollResults} from "../../../models/poll-results";
import {Chart, registerables} from "chart.js";

@Component({
  selector: 'app-pie-chart',
  templateUrl: './pie-chart.component.html',
  styleUrls: ['./pie-chart.component.scss']
})
export class PieChartComponent implements OnInit {
  @Input() results: IPollResults[];
  chart: Chart;

  constructor() {
    Chart.register(...registerables);
  }

  ngOnInit(): void {
    this.chart = new Chart('canvas-pie', {
      type: 'pie',
      data: {
        labels: this.results.map(r => r.lastName),
        datasets: [{
          label: 'Results',
          data: this.results.map(r => r.percents),
          backgroundColor: 'rgba(54, 162, 235, 0.5)',
          borderColor: 'rgb(54, 162, 235)',
          borderWidth: 1
        }]
      }
    });
  }

}
