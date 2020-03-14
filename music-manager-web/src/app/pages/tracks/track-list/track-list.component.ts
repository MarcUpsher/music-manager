import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-track-list',
  templateUrl: './track-list.component.html',
  styleUrls: ['./track-list.component.scss'],
  providers: []
})
export class TrackListComponent implements OnInit {
  loading = true;

  constructor(
  ) {
  }

  ngOnInit() {
  }
}
