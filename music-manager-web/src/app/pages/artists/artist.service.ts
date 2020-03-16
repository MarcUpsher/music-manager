import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { Artist, ArtistWithAlbums, ArtistPostDTO } from '../../models/artist';
import { FilterItem } from 'src/app/models/filter-item';

@Injectable({
  providedIn: 'root'
})
export class ArtistService {

  url = '';
  headers = new HttpHeaders().append('Content-Disposition', 'mulipart/form-data');

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

  getArtist(id: number): Observable<ArtistWithAlbums> {
    return this.httpClient.get<ArtistWithAlbums>(this.url + '/api/artists/' + id)
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

  addArtist(artistPostDTO: FormData): Observable<Artist> {
    return this.httpClient.post<Artist>(this.url + '/api/artists', artistPostDTO, {headers: this.headers})
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  updateArtist(id: number, artistPostDTO: FormData): Observable<Artist> {
    return this.httpClient.put<Artist>(this.url + '/api/artists/' + id, artistPostDTO)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  doesArtistExist(name: string): Observable<boolean> {
    return this.httpClient.get<boolean>(this.url + '/api/artists/doesgenreexist?name=' + encodeURI(name))
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  deleteArtist(id: number): Observable<Artist> {
    return this.httpClient.delete<Artist>(this.url + '/api/artists/' + id)
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
