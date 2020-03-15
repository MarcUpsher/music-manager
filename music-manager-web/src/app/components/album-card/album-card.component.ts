import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Album } from '../../models/album';

@Component({
  selector: 'app-album-card',
  templateUrl: './album-card.component.html',
  styleUrls: ['./album-card.component.scss'],
  providers: []
})
export class AlbumCardComponent implements OnInit {
  @Input() album: Album;
  @Output() albumOutput = new EventEmitter<Album>();

  constructor(
  ) {
  }

  ngOnInit() {
  }

  onClick() {
    this.albumOutput.emit(this.album);
  }
}
