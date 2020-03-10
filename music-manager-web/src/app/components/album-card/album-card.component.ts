import { Component, OnInit, Input } from '@angular/core';
import { Album } from '../../models/album';

@Component({
  selector: 'app-album-card',
  templateUrl: './album-card.component.html',
  styleUrls: ['./album-card.component.scss'],
  providers: []
})
export class AlbumCardComponent implements OnInit {
  @Input() album: Album;

  constructor(
  ) {
  }

  ngOnInit() {
    console.log(this.album);
  }
}
