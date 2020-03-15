import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../artist.service';
import { ArtistWithAlbums } from 'src/app/models/artist';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-artist-view',
  templateUrl: './artist-view.component.html',
  styleUrls: ['./artist-view.component.scss'],
  providers: []
})
export class ArtistViewComponent implements OnInit {
  loading = true;
  title = '';
  artistId: number;
  artist: ArtistWithAlbums;

  constructor(
    public artistService: ArtistService,
    public activatedRoute: ActivatedRoute,
  ) {
    this.artistId = this.activatedRoute.snapshot.params.id;
  }

  ngOnInit() {
    this.getArtist(this.artistId);
  }

  getArtist(id: number) {
    this.loading = false;
    this.artistService.getArtist(id).subscribe((data: ArtistWithAlbums) => {
      this.title = data.name;

      this.artist = data;

      this.loading = false;
    });
  }
}

