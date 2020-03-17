import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Album } from '../models/album';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  constructor() { }

  private viewAlbumEvent = new BehaviorSubject<Album>(null);
  private editAlbumEvent = new BehaviorSubject<Album>(null);

  // Album card
  emitViewAlbumEvent(album: Album) {
    this.viewAlbumEvent.next(album);
  }

  viewAlbumEventListener() {
    return this.viewAlbumEvent.asObservable();
  }

  // Edit album
  emitEditAlbumEvent(album: Album) {
    this.editAlbumEvent.next(album);
  }

  editAlbumEventListener() {
    return this.editAlbumEvent.asObservable();
  }
}
