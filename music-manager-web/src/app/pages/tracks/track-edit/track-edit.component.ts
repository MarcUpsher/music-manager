import { Component, OnInit } from '@angular/core';
import { TrackService } from '../track.service';

@Component({
  selector: 'app-track-edit',
  templateUrl: './track-edit.component.html',
  styleUrls: ['./track-edit.component.scss'],
  providers: []
})
export class TrackEditComponent implements OnInit {
  loading = true;

  constructor(
  ) {
  }

  ngOnInit() {
  }
}
