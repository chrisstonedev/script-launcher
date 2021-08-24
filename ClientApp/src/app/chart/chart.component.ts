import {Component, OnInit} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {SignalRService} from "../signal-r.service";

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})

export class ChartComponent implements OnInit {
  chartOptions = {
    scaleShowVerticalLines: true,
    responsive: true,
    scales: {
      yAxes: [{
        ticks: {
          beginAtZero: true
        }
      }]
    }
  };
  chartLabels: string[] = ['Real time data for the chart'];
  colors = [
    {backgroundColor: '#5491DA'},
    {backgroundColor: '#E74C3C'},
    {backgroundColor: '#82E0AA'},
    {backgroundColor: '#E5E7E9'}
  ];

  constructor(private http: HttpClient, public signalRService: SignalRService) {
  }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();
    this.signalRService.addBroadcastChartDataListener();
    this.startHttpRequest();
  }

  chartClicked() {
    this.signalRService.broadcastChartData();
  }

  startHttpRequest = () => {
    this.http.get('https://localhost:5001/api/chart').subscribe();
  }
}
