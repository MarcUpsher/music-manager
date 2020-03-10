import { Component, OnInit } from '@angular/core';
import { AlbumService } from '../album.service';
import { Album } from '../../../models/album';
import { LoaderComponent } from '../../../components/loader/loader.component';
import { AlbumCardComponent } from '../../../components/album-card/album-card.component';

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
    this.getAlbums();
  }

  getAlbums() {
    return this.albumService.getAlbums().subscribe((data: Album[]) => {
      this.albums = data;
      this.loading = false;
    });
  }
}
