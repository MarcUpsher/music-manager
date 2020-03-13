import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Artist } from '../../models/artist';
import { FilterItem } from 'src/app/models/filter-item';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {

  url = '';

  constructor(private httpClient: HttpClient) {
    this.url = environment.url;
  }

  getArtists(): Observable<Artist[]> {
    return this.httpClient.get<Artist[]>(this.url + '/api/artists/')
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getArtist(id: number): Observable<Artist> {
    return this.httpClient.get<Artist>(this.url + '/api/artists/' + id)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getArtistsForFilter(): Observable<FilterItem[]> {
    return this.httpClient.get<FilterItem[]>(this.url + '/api/artistsforfilter')
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
