import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AlbumService } from '../album.service';
import { Album } from '../../../models/album';
import { EventService } from 'src/app/shared/event.service';

@Component({
  selector: 'app-album-view',
  templateUrl: './album-view.component.html',
  styleUrls: ['./album-view.component.scss'],
  providers: []
})
export class AlbumViewComponent implements OnInit {
  @Input() album: Album;
  @Output() closeAlbumView = new EventEmitter<true>();
  loading = true;
  albumId: number;
  showEdit = false;
  genres = '';

  displayedColumns: string[] = ['position', 'name', 'duration'];

  constructor(
    public albumService: AlbumService,
    public eventService: EventService,
    public router: Router
  ) {
  }

  ngOnInit() {
    this.loading = false;

    this.genres = this.album.genres.length > 0 ? this.album.genres.toString() : 'None specified';
  }

  onEditAlbumClick() {
    this.eventService.emitEditAlbumEvent(this.album);
  }

  onCloseView() {
    this.closeAlbumView.emit(true);
  }
}
