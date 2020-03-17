import { Component, OnInit, Input } from '@angular/core';
import { Album } from '../../models/album';
import { EventService } from '../../shared/event.service';

@Component({
  selector: 'app-album-card',
  templateUrl: './album-card.component.html',
  styleUrls: ['./album-card.component.scss'],
  providers: []
})
export class AlbumCardComponent implements OnInit {
  @Input() album: Album;

  constructor(
    public eventService: EventService
  ) {
  }

  ngOnInit() {
  }

  onClick() {
    this.eventService.emitViewAlbumEvent(this.album);
  }
}
