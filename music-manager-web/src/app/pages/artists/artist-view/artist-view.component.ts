import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { ArtistService } from '../artist.service';
import { ArtistWithAlbums } from 'src/app/models/artist';
import { ActivatedRoute, Router } from '@angular/router';
import { Album } from 'src/app/models/album';
import { MatDialog } from '@angular/material/dialog';
import { ArtistEditComponent } from '../artist-edit/artist-edit.component';
import { Location } from '@angular/common';
import { EventService } from '../../../shared/event.service';

@Component({
  selector: 'app-artist-view',
  templateUrl: './artist-view.component.html',
  styleUrls: ['./artist-view.component.scss'],
  providers: []
})
export class ArtistViewComponent implements OnInit {
  loading = true;
  title = '';
  showAlbumList = true;
  showAlbum = false;
  addOrEditAlbum = false;
  isAdd = false;
  isEdit = false;
  artistId: any;
  albumId: any;
  album: Album = null;
  artist: ArtistWithAlbums;

  constructor(
    public artistService: ArtistService,
    public eventService: EventService,
    public activatedRoute: ActivatedRoute,
    public router: Router,
    public location: Location,
    public dialog: MatDialog
  ) {
    this.artistId = this.activatedRoute.snapshot.params.artistId;
    this.albumId = this.activatedRoute.snapshot.params.albumId;

    if (this.activatedRoute.snapshot.url.length === 3 
      && this.activatedRoute.snapshot.url[2].toString().toLowerCase() === 'add-album') {
        this.isAdd = true;
    }

    if (this.activatedRoute.snapshot.url.length === 5 
      && this.activatedRoute.snapshot.url[4].toString().toLowerCase() === 'edit') {
        this.isEdit = true;
    }
  }

  ngOnInit() {
    this.getArtist(this.artistId);

    this.eventService.viewAlbumEventListener().subscribe(album => {
      if (album != null) {
        this.album = album;
        this.switchViews('view');
      }
    });

    this.eventService.editAlbumEventListener().subscribe(album => {
      if (album != null) {
        this.album = album;
        this.switchViews('addOrEdit');
      }
    });
  }

  switchViews(view: string) {
    switch (view) {
      case 'list':
        this.showAlbumList = true;
        this.addOrEditAlbum = false;
        this.showAlbum = false;
        this.updateUrl(['/artist', this.artistId]);
        break;

      case 'view':
        this.showAlbumList = false;
        this.addOrEditAlbum = false;
        this.showAlbum = true;
        this.updateUrl(['/artist', this.artistId, 'album', this.album.id]);
        break;

      case 'addOrEdit':
        this.showAlbumList = false;
        this.addOrEditAlbum = true;
        this.showAlbum = false;
        if (this.album == null) {
          this.updateUrl(['/artist', this.artistId, 'add-album']);
        } else {
          this.updateUrl(['/artist', this.artistId, 'album', this.album.id, 'edit']);
        }
        break;
    }
  }

  updateUrl(params: any[]) {
    const url = this
    .router
    .createUrlTree(params)
    .toString();

    this.location.go(url);
  }

  getArtist(id: number) {
    this.loading = true;

    this.artistService.getArtist(id).subscribe((data: ArtistWithAlbums) => {
      this.title = data.name;

      this.artist = data;

      if (this.isAdd || this.isEdit) {
        this.switchViews('addOrEdit');
      } else if (parseInt(this.albumId, 10) > 0) {
        this.album = this.artist.albums.find(item => item.id == this.albumId); // === fails comparison
        if (this.album != null) {
          this.switchViews('view');
        }
      }

      this.loading = false;
    });
  }

  showEditDialog() {
    const dialog = this.dialog.open(ArtistEditComponent, {
      width: '500px',
      data: this.artistId
    });

    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.switchViews('list');

        this.getArtist(this.artistId);
      }
    });
  }

  onCloseAlbumView() {
    this.switchViews('list');
  }

  showAddAlbum() {
    this.album = null;
    this.switchViews('addOrEdit');
  }

  onCloseView() {
    this.switchViews('list');
  }
}

