import { Component, OnInit, Inject } from '@angular/core';
import { GenreService } from '../genre.service';
import { Genre } from 'src/app/models/genre';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-genre-edit',
  templateUrl: './genre-edit.component.html',
  styleUrls: ['./genre-edit.component.scss'],
  providers: []
})
export class GenreEditComponent implements OnInit {
  loading = true;
  title: string;
  genreForm: FormGroup;

  constructor(
    public genreService: GenreService,
    public dialogRef: MatDialogRef<GenreEditComponent>,
    @Inject(MAT_DIALOG_DATA) public id: number,
    public fb: FormBuilder
  ) {
    this.genreForm = this.fb.group({
      name: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.title = 'Add new genre';

    if (this.id != null) {
      this.getGenre(this.id);
    }
  }

  getGenre(id: number) {
    this.genreService.getGenre(id).subscribe((data: Genre) => {
      this.title = 'Edit genre ' + data.name;

      this.genreForm.patchValue({
        name: data.name
      });

      this.loading = false;
    });
  }

  submit() {

  }
}
