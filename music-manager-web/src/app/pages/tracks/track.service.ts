import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Track } from '../../models/track';

@Injectable({
  providedIn: 'root'
})
export class TrackService {
  url = '';

  constructor(private httpClient: HttpClient) {
    this.url = environment.url;
  }

  getTracks(): Observable<Track[]> {
    return this.httpClient.get<Track[]>(this.url + '/api/track/')
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getGenre(id: number): Observable<Track> {
    return this.httpClient.get<Track>(this.url + '/api/track/' + id)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  // Error handling
  errorHandler(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
 }
}
