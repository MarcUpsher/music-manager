import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AlbumService } from '../album.service';
import { Album } from '../../../models/album';
import { FormGroup, FormControl } from '@angular/forms';

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
  albumForm: FormGroup;

  displayedColumns: string[] = ['position', 'name', 'duration'];

  constructor(
    public albumService: AlbumService,
    public router: Router,
    public activatedRoute: ActivatedRoute,
    public dialog: MatDialog
  ) {
    this.albumId = this.activatedRoute.snapshot.params.id;
  }

  ngOnInit() {
    this.albumForm = new FormGroup({
      name: new FormControl(''),
      artistId: new FormControl(''),
      releaseDate: new FormControl(''),
      summary: new FormControl(''),
      genres: new FormControl(''),
    });

    this.getAlbum(this.albumId);
  }

  getAlbum(id: number) {
    return this.albumService.getAlbum(id).subscribe((data: Album) => {
      this.album = data;
      this.loading = false;
    });
  }
}
