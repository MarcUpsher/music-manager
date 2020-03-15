import { Component, OnInit, Inject } from '@angular/core';
import { GenreService } from '../genre.service';
import { Genre, GenrePostDTO } from 'src/app/models/genre';
import { FormGroup, FormBuilder, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable, combineLatest } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';

@Component({
  selector: 'app-genre-edit',
  templateUrl: './genre-edit.component.html',
  styleUrls: ['./genre-edit.component.scss'],
  providers: []
})
export class GenreEditComponent implements OnInit {
  loading = false;
  isEdit = false;
  title: string;
  validating = false;
  currentName = '';
  genreForm: FormGroup;

  constructor(
    public genreService: GenreService,
    public dialogRef: MatDialogRef<GenreEditComponent>,
    @Inject(MAT_DIALOG_DATA) public id: number,
    public fb: FormBuilder
  ) {
    this.genreForm = this.fb.group({
      name: ['', [Validators.required], this.validateName.bind(this)]
    });
  }

  ngOnInit() {
    this.title = 'Add new genre';

    if (this.id != null) {
      this.loading = true;
      this.isEdit = true;
      this.getGenre(this.id);
    }
  }

  get name() {
    return this.genreForm.get('name');
  }

  public errorHandling = (control: string, error: string) => {
    return this.genreForm.controls[control].hasError(error);
  }

  validateName({ value }: AbstractControl): Observable<ValidationErrors | null> {
    const validations: Observable<boolean>[] = [];
    const nameExists$: Observable<boolean> = this.genreService.doesGenreExist(value);
    validations.push(nameExists$);

    this.validating = true;
    return combineLatest(validations)
      .pipe(debounceTime(500), map(([nameExists]) => {
        this.validating = false;
        if (nameExists && !(this.isEdit && (value.toString().toLowerCase() === this.currentName.toLowerCase()))) {
          return {
            invalidName: true
          };
        }
        return null;
      }));
  }

  isFormInvalid() {
    if (this.loading || (!this.validating && !this.genreForm.valid)) {
      return true;
    }

    if (this.isEdit) {
      if (this.name.value.toLowerCase() === this.currentName.toLowerCase()) {
        return true;
      }
    }

    return false;
  }

  getGenre(id: number) {
    this.genreService.getGenre(id).subscribe((data: Genre) => {
      this.title = 'Edit genre \'' + data.name + '\'';
      this.currentName = data.name;

      this.genreForm.patchValue({
        name: data.name
      });

      this.loading = false;
    });
  }

  submit() {
    const genreUpdateDTO = new GenrePostDTO(this.genreForm.value.name);

    if (this.isEdit) {
      this.genreService.updateGenre(this.id, genreUpdateDTO).subscribe((data: Genre) => {
        if (data.id === this.id) {
          this.dialogRef.close(true);
        }
      });
    } else {
      this.genreService.addGenre(genreUpdateDTO).subscribe((data: Genre) => {
        if (data != null && data.id > 0) {
          this.dialogRef.close(true);
        }
      });
    }
  }
}
