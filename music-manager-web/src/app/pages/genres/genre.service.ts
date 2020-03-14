import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Genre } from '../../models/genre';
import { FilterItem } from 'src/app/models/filter-item';

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  url = '';

  constructor(private httpClient: HttpClient) {
    this.url = environment.url;
  }

  getGenres(): Observable<Genre[]> {
    return this.httpClient.get<Genre[]>(this.url + '/api/genres')
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getGenre(id: number): Observable<Genre> {
    return this.httpClient.get<Genre>(this.url + '/api/genres/' + id)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getGenresForFilter(): Observable<FilterItem[]> {
    return this.httpClient.get<FilterItem[]>(this.url + '/api/genresforfilter')
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