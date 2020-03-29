import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Album, AlbumPost } from '../../models/album';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AlbumService {

  url = '';
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8'
    })
  };

  constructor(private httpClient: HttpClient) {
    this.url = environment.url;
  }

  getAlbums(): Observable<Album[]> {
    return this.httpClient.get<Album[]>(this.url + '/api/albums/')
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getAlbumsByArtistId(artistId: number): Observable<Album[]> {
    return this.httpClient.get<Album[]>(this.url + '/api/albumsbyartist/' + artistId)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  getAlbum(id: number): Observable<Album> {
    return this.httpClient.get<Album>(this.url + '/api/albums/' + id)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  addAlbum(albumPostDTO: FormData): Observable<Album> {
    return this.httpClient.post<Album>(this.url + '/api/albums', albumPostDTO)
    .pipe(
      retry(1),
      catchError(this.errorHandler)
    );
  }

  updateAlbum(id: number, albumPostDTO: FormData): Observable<Album> {
    return this.httpClient.put<Album>(this.url + '/api/albums/' + id, albumPostDTO)
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
