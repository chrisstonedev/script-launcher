import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-run-script',
  templateUrl: './run-script.component.html'
})
export class RunScriptComponent implements OnInit {
  public scripts: Script[] = [];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
  }

  ngOnInit(): void {
    this.http.get<Script[]>(this.baseUrl + 'scripts').subscribe({
      next: x => this.scripts = x,
      error: error => console.error(error)
    });
  }

  executeScript(script: Script): void {
    this.executeScriptPost(script).subscribe(result => {
      console.log(result);
    });
  }

  executeScriptPost(script: Script): Observable<Script> {
    return this.http.post(this.baseUrl + 'scripts', script)
      .pipe(map((response: any) => response));
  }
}

interface Script {
  filePath: string;
  fileName: string;
}
