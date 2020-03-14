import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-artist-view',
  templateUrl: './artist-view.component.html',
  styleUrls: ['./artist-view.component.scss'],
  providers: []
})
export class ArtistViewComponent implements OnInit {
  loading = true;

  constructor(
  ) {
  }

  ngOnInit() {
  }
}
