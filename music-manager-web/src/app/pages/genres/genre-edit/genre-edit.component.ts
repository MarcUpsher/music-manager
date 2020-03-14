import { Component, OnInit } from '@angular/core';
import { GenreService } from '../genre.service';

@Component({
  selector: 'app-genre-edit',
  templateUrl: './genre-edit.component.html',
  styleUrls: ['./genre-edit.component.scss'],
  providers: []
})
export class GenreEditComponent implements OnInit {
  loading = true;

  constructor(
  ) {
  }

  ngOnInit() {
  }
}
