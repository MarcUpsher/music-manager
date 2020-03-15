import { Component, OnInit, Input } from '@angular/core';
import { AlbumService } from '../album.service';
import { Album } from '../../../models/album';

@Component({
  selector: 'app-album-list',
  templateUrl: './album-list.component.html',
  styleUrls: ['./album-list.component.scss'],
  providers: []
})
export class AlbumListComponent implements OnInit {
  @Input() albums: Album[];
  loading = true;

  constructor(
    public albumService: AlbumService
  ) {
  }

  ngOnInit() {
    this.loading = false;
    //this.getAlbumsByArtist();
  }

  getAlbumsByArtist() {
    return this.albumService.getAlbums().subscribe((data: Album[]) => {
      this.albums = data;
      this.loading = false;
    });
  }
}
