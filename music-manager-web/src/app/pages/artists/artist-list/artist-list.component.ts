import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../artist.service';
import { Artist } from 'src/app/models/artist';

@Component({
  selector: 'app-artist-list',
  templateUrl: './artist-list.component.html',
  styleUrls: ['./artist-list.component.scss'],
  providers: []
})
export class ArtistListComponent implements OnInit {
  loading = true;
  title = 'Artists';
  artists: Artist[] = [];

  constructor(
    public artistService: ArtistService
  ) {
  }

  ngOnInit() {
    this.getArtists();
  }

  getArtists() {
    this.loading = true;

    this.artistService.getArtists().subscribe((data: Artist[]) => {
      this.artists = data;
      this.loading = false;
    });
  }
}
