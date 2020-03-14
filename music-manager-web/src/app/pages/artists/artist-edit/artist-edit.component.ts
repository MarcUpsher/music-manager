import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../../artists/artist.service';

@Component({
  selector: 'app-artist-edit',
  templateUrl: './artist-edit.component.html',
  styleUrls: ['./artist-edit.component.scss'],
  providers: []
})
export class ArtistEditComponent implements OnInit {
  loading = true;

  constructor(
    public artistService: ArtistService,
  ) {
  }

  ngOnInit() {
  }
}
