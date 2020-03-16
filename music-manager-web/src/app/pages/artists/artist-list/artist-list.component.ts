import { Component, OnInit } from '@angular/core';
import { ArtistService } from '../artist.service';
import { Artist } from 'src/app/models/artist';
import { ArtistEditComponent } from '../artist-edit/artist-edit.component';
import { MatDialog } from '@angular/material/dialog';

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
    public artistService: ArtistService,
    public dialog: MatDialog
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

  showAddArtist() {
    const dialog = this.dialog.open(ArtistEditComponent, {
      width: '500px',
      data: null
    });

    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.getArtists();
      }
    });
  }
}
