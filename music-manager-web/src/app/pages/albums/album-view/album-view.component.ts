import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AlbumService } from '../album.service';
import { Album } from '../../../models/album';

@Component({
  selector: 'app-album-view',
  templateUrl: './album-view.component.html',
  styleUrls: ['./album-view.component.scss'],
  providers: []
})
export class AlbumViewComponent implements OnInit {
  loading = true;
  album: Album;
  albumId: number;

  constructor(
    public albumService: AlbumService,
    public activatedRoute: ActivatedRoute
  ) {
    this.albumId = this.activatedRoute.snapshot.params.id;
  }

  ngOnInit() {
    this.getAlbum(this.albumId);
  }

  getAlbum(id: number) {
    return this.albumService.getAlbum(id).subscribe((data: Album) => {
      this.album = data;
      this.loading = false;
    });
  }
}
