import { Component, OnInit, Input } from '@angular/core';
import { Artist } from '../../models/artist';

@Component({
  selector: 'app-artist-card',
  templateUrl: './artist-card.component.html',
  styleUrls: ['./artist-card.component.scss'],
  providers: []
})
export class ArtistCardComponent implements OnInit {
  @Input() artist: Artist;

  constructor(
  ) {
  }

  ngOnInit() {
  }
}
