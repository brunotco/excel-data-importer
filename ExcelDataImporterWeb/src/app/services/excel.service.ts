import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';

@Injectable({
  providedIn: 'root'
})
export class ExcelService {

  private data: any[] = [];

  constructor() { }

  fileLoad(event: Event): string {
    const target = <HTMLInputElement>event.target;
    if (target.files?.length !== 1) throw new Error('Cannot use multiple files.');
    const reader = new FileReader();
    reader.onload = (e: any) => {
      // Read workbook
      const bstr: string = e.target.result;
      const wb: XLSX.WorkBook = XLSX.read(bstr, { type: 'binary' });
      // Grab first sheet
      const wsname: string = wb.SheetNames[0];
      const ws: XLSX.WorkSheet = wb.Sheets[wsname];
      // Store data
      this.data = <any[]>XLSX.utils.sheet_to_json(ws);
      // console.log(this.data);
    };
    reader.readAsBinaryString(target.files[0]);
    return target.files[0].name;
  }

  writeFile(data: any[]) {
    // Create worksheet
    const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
    // Create workbook
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'Data');
    // Save to file
    XLSX.writeFile(wb, "exported.xlsx");
  }

  clearData() {
    this.data = [];
  }

  getData(): any[] {
    return this.data;
  }
}
