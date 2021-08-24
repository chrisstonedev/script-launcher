import {Injectable} from "@angular/core";
import * as signalR from "@microsoft/signalr";

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  public data: ChartModel[] = [];
  public broadcastData: ChartModel[] = [];

  private hubConnection: signalR.HubConnection | null = null;

  public startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:5001/chart')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.error('Error while starting connection: ' + err));
  }

  public addTransferChartDataListener() {
    // noinspection SpellCheckingInspection
    this.hubConnection?.on('transferchartdata', (data) => {
      this.data = data;
    });
  }

  public broadcastChartData() {
    const data = this.data.map(m => {
      return {
        data: m.data,
        label: m.label
      };
    });

    // noinspection SpellCheckingInspection
    this.hubConnection?.invoke('broadcastchartdata', data)
      .catch(err => console.error(err));
  }

  public addBroadcastChartDataListener = () => {
    // noinspection SpellCheckingInspection
    this.hubConnection?.on('broadcastchartdata', (data) => {
      this.broadcastData = data;
    })
  }
}

export interface ChartModel {
  data: any[],
  label: string
}
