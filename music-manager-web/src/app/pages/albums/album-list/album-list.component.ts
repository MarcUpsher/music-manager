import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
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
  @Output() albumOutput = new EventEmitter<Album>();
  loading = true;
  showAlbum = false;

  constructor(
    public albumService: AlbumService
  ) {
  }

  ngOnInit() {
    this.loading = false;
  }

  getAlbumsByArtist() {
    return this.albumService.getAlbums().subscribe((data: Album[]) => {
      this.albums = data;
      this.loading = false;
    });
  }

  onAlbumCardClick(album: any) {
    this.albumOutput.emit(album);
  }

  onCloseView() {
    this.showAlbum = false;
  }
}
