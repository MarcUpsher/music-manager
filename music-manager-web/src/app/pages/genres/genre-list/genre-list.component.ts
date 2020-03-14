import { Component, OnInit } from '@angular/core';
import { GenreService } from '../genre.service';
import { Genre } from '../../../models/genre';

@Component({
  selector: 'app-genre-list',
  templateUrl: './genre-list.component.html',
  styleUrls: ['./genre-list.component.scss'],
  providers: []
})
export class GenreListComponent implements OnInit {
  loading = true;
  displayedColumns: string[] = ['name', 'associated-albums', 'actions'];
  dataSource: Genre[] = [];

  constructor(
    public genreService: GenreService
  ) {
  }

  ngOnInit() {
    this.getGenres();
  }

  getGenres() {
    return this.genreService.getGenres().subscribe((data: Genre[]) => {
      this.dataSource = data;
      this.loading = false;
    });
  }
}
