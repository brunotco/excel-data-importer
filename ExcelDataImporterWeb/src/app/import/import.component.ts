import { Component, OnInit } from '@angular/core';
import { User } from '../shared/user';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.css']
})
export class ImportComponent implements OnInit {
  public userData: User[] = [];

  constructor(
    private apiService: ApiService
  ) { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.load();
  }

  load() {
    this.apiService.loadUser()
    .subscribe(result => {
      this.userData = result;
    });
  }
}
