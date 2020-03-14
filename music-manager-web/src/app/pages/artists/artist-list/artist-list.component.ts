import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-artist-list',
  templateUrl: './artist-list.component.html',
  styleUrls: ['./artist-list.component.scss'],
  providers: []
})
export class ArtistListComponent implements OnInit {
  loading = true;

  constructor(
  ) {
  }

  ngOnInit() {
  }
}
