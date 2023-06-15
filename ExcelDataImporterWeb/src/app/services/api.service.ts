import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { User } from '../shared/user';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = "/api/user"

  constructor(private http: HttpClient) { }

  loadUser(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return this.handleError(error);
      })
    );
  }

  markActiveUser(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/active/${ id }`, {})
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return this.handleError(error);
      })
    );
  }

  markInactiveUser(id: number): Observable<any> {
    return this.http.put(`${this.baseUrl}/inactive/${ id }`, {})
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return this.handleError(error);
      })
    );
  }

  newRecord(user: User): Observable<User> {
    return this.http.post<User>(this.baseUrl, user)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return this.handleError(error);
      })
    );
  }

  loadAvailable(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseUrl}/available`)
    .pipe(
      catchError((error: HttpErrorResponse) => {
        return this.handleError(error);
      })
    );
  }

  private handleResponse<T>(response: HttpResponse<T>) {
    console.log('Status code:', response.status);
    if (response.body === null)
      return [];
    return response.body
  }

  private handleError(error: HttpErrorResponse) {
    if (error.status === 0) {
      // A client-side or network error occurred. Handle it accordingly.
      console.error('An error occurred:', error.error);
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong.
      console.error(
        `Backend returned code ${error.status}: `, { details: error.error || error.name});
    }
    // Return an observable with a user-facing error message.
    return throwError(error);
  }
}
