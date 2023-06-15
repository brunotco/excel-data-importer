import { Component, OnInit } from '@angular/core';
import { User } from '../shared/user';
import { ApiService } from '../services/api.service';
import { MatLegacyTableDataSource as MatTableDataSource } from '@angular/material/legacy-table';
import { ExcelService } from '../services/excel.service';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {

  public userData: User[] = [];
  public columnHeaders: string[] = Object.keys(new User());
  public tableData = new MatTableDataSource<(User)>(this.userData);

  constructor(
    private apiService: ApiService,
    private excelService: ExcelService
  ) { }

  ngOnInit(): void {
  }

  async ngAfterViewInit() {
    this.load();
  }

  load() {
    this.apiService.loadUser()
    .subscribe(result => {
      this.userData = result;
      this.tableData.data = result;
    });
  }

  // async waitLoad() {
  //   return await this.apiService.loadUser().toPromise();
  // }

  mark(id: number, completed: boolean) {
    console.log(id);
    if (!completed) {
      this.apiService.markActiveUser(id)
      .subscribe(() => {
        this.load();
      });
    }
    else if (completed) {
      this.apiService.markInactiveUser(id)
      .subscribe(() => {
        this.load();
      });
    }
  }

  allActive() {
    this.userData.forEach(user => {
      if(user.active === false)
        this.apiService.markActiveUser(user.id)
        .subscribe(() => {
          this.load();
        });
    });
  }

  allInactive() {
    this.userData.forEach(user => {
      if(user.active === true)
        this.apiService.markInactiveUser(user.id)
        .subscribe(() => {
          this.load();
        });
    });
  }

  export() {
    this.excelService.writeFile(this.userData);
  }

}
