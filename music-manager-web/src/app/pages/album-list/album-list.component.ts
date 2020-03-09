import { Component, OnInit } from '@angular/core';
import { AlbumService } from './album-list.service';
import { Album } from 'src/models/album';

@Component({
  selector: 'app-album-list',
  templateUrl: './album-list.component.html',
  styleUrls: ['./album-list.component.scss'],
  providers: []
})
export class AlbumListComponent implements OnInit {
  loading = true;
  albums: Album[] = [];

  constructor(
    public albumService: AlbumService
  ) {
  }

  ngOnInit() {
    console.log('here');
    this.getAlbums();
  }

  getAlbums() {
    return this.albumService.getAlbums().subscribe((data: Album[]) => {
      this.albums = data;
      this.loading = false;
    });
  }
}
