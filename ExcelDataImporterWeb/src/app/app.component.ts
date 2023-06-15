import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public title = 'Excel Data Importer';
  public active = 'import';

  activate(option: 'import' | 'report') {
    this.active = option;
  }
}