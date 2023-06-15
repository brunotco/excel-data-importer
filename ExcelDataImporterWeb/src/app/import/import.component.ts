import { Component, OnInit } from '@angular/core';
import { User } from '../shared/user';
import { ApiService } from '../services/api.service';
import { ExcelService } from '../services/excel.service';

@Component({
  selector: 'app-import',
  templateUrl: './import.component.html',
  styleUrls: ['./import.component.css']
})
export class ImportComponent implements OnInit {
  public formats: string = '.xls, .xlsx';
  public finishedImport = true;
  public fileName: string = '';
  public importedRows: number[] = [];
  public skippedRows: Map<number, string> = new Map<number, string>();

  constructor(
    private apiService: ApiService,
    private excelService: ExcelService
  ) { }

  ngOnInit(): void {
  }

  fileLoad(event: Event) {
    this.fileName = this.excelService.fileLoad(event);
    this.finishedImport = false;
  }

  clear() {
    this.excelService.clearData();
    this.fileName = '';
    this.finishedImport = true;
    this.importedRows = [];
    this.skippedRows.clear();
  }

  upload() {
    const data = this.excelService.getData();

    data.forEach((row: User, index) => {
      const lineNumber = index + 2;
      if (!row.fullname || !row.username || !row.password || !row.email) {
        this.skippedRows.set(lineNumber, "missing fields");
        return;
      }
      const user = new User(row.fullname, row.username, row.password, row.email)
      this.apiService.newRecord(user)
      .subscribe(() => {
        this.importedRows.push(lineNumber);
      }, error => {
        console.error(error.status);
        if (error.status === 400) {
          this.skippedRows.set(lineNumber, "duplicated entry");
        } else {
          this.skippedRows.set(lineNumber, error.error);
        };
      });
    });

    this.fileName = '';
  }

  toImport() {
    return this.excelService.getData().length;
  }
}
