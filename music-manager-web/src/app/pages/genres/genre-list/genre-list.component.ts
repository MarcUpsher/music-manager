import { Component, OnInit, ViewChild } from '@angular/core';
import { GenreService } from '../genre.service';
import { Genre } from '../../../models/genre';
import { MatMenuTrigger } from '@angular/material/menu';
import { GenreEditComponent } from '../genre-edit/genre-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../components/confirm-dialog/confirm-dialog.component';
import { MatTable } from '@angular/material/table';

@Component({
  selector: 'app-genre-list',
  templateUrl: './genre-list.component.html',
  styleUrls: ['./genre-list.component.scss'],
  providers: []
})
export class GenreListComponent implements OnInit {
  loading = true;
  title = 'Genres';
  displayedColumns: string[] = ['name', 'associatedAlbums', 'actions'];
  dataSource: Genre[] = [];
  @ViewChild(MatTable) table: MatTable<any>;
  @ViewChild(MatMenuTrigger) trigger: MatMenuTrigger;

  constructor(
    public genreService: GenreService,
    public dialog: MatDialog
  ) {
  }

  ngOnInit() {
    this.getGenres();
  }

  getGenres() {
    this.loading = true;

    return this.genreService.getGenres().subscribe((data: Genre[]) => {
      this.dataSource = data;
      this.loading = false;
    });
  }

  refreshTable() {
    this.getGenres();
    this.table.renderRows();
  }

  showEditDialog(item: any = null) {
    const dialog = this.dialog.open(GenreEditComponent, {
      width: '400px',
      data: item != null ? item.id : null
    });

    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.refreshTable();
      }
    });
  }

  showDeleteDialog(item: any) {
    const dialog = this.dialog.open(ConfirmDialogComponent, {
      width: '250px'
    });

    dialog.afterClosed().subscribe(result => {
      if (result) {
        this.genreService.deleteGenre(item.id).subscribe((data: Genre) => {
          if (data.id === item.id) {
            this.refreshTable();
          }
        });
      }
    });
  }
}
