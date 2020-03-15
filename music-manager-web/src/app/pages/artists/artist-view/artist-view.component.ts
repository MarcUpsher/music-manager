import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ArtistService } from '../artist.service';
import { ArtistWithAlbums } from 'src/app/models/artist';
import { ActivatedRoute } from '@angular/router';
import { Album } from 'src/app/models/album';

@Component({
  selector: 'app-artist-view',
  templateUrl: './artist-view.component.html',
  styleUrls: ['./artist-view.component.scss'],
  providers: []
})
export class ArtistViewComponent implements OnInit {  
  loading = true;
  title = '';
  showAlbum = false;
  artistId: number;
  album: Album;
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
    this.loading = true;
    this.artistService.getArtist(id).subscribe((data: ArtistWithAlbums) => {
      this.title = data.name;

      this.artist = data;

      this.loading = false;
    });
  }

  onAlbumClick(album: Album) {
    this.album = album;
    this.showAlbum = true;
  }

  onCloseView() {
    this.showAlbum = false;
  }
}

