import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { ArtistService } from '../../artists/artist.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { ArtistPostDTO, Artist, ArtistWithAlbums } from 'src/app/models/artist';
import { FileValidator, FileInput } from 'ngx-material-file-input';

@Component({
  selector: 'app-artist-edit',
  templateUrl: './artist-edit.component.html',
  styleUrls: ['./artist-edit.component.scss'],
  providers: []
})
export class ArtistEditComponent implements OnInit {
  loading = false;
  title = 'Add new artist';
  isEdit = false;
  artistForm: FormGroup;
  validating = false;
  currentName: string;
  currentImageUriId: number;
  imageURL = '';
  @ViewChild('fileInput', { static: false }) fileInput: any;
  readonly maxSize = 1048576;

  constructor(
    public artistService: ArtistService,
    public dialogRef: MatDialogRef<ArtistEditComponent>,
    @Inject(MAT_DIALOG_DATA) public id: number,
    public fb: FormBuilder
  ) {
    this.artistForm = this.fb.group({
      name: ['', [Validators.required]],
      summary: [''],
      image: [undefined, [Validators.required, FileValidator.maxContentSize(this.maxSize)]
      ]
    });
  }

  ngOnInit() {
    if (this.id != null) {
      this.loading = true;
      this.isEdit = true;
      this.getArtist(this.id);
    }
  }

  getArtist(id: number) {
    this.artistService.getArtist(id).subscribe((data: ArtistWithAlbums) => {
      this.title = 'Edit artist \'' + data.name + '\'';
      this.currentName = data.name;
      this.currentImageUriId = data.imageUriId;
      if (data.imageUri !== '') {
        this.imageURL = data.imageUri;
      }

      this.artistForm.patchValue({
        name: data.name,
        summary: data.summary
      });

      this.loading = false;
    });
  }

  get name() {
    return this.artistForm.get('name');
  }

  get image() {
    return this.artistForm.get('image');
  }

  get summary() {
    return this.artistForm.get('summary');
  }

  public errorHandling = (control: string, error: string) => {
    return this.artistForm.controls[control].hasError(error);
  }

  clearImage() {
    this.imageURL = '';
    this.artistForm.get('image').updateValueAndValidity();
  }

  onFileSelect(event: any) {
    const file = this.artistForm.get('image').value.files[0] as File;

    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  isFormInvalid() {
    if (this.loading || (!this.validating && !this.artistForm.valid)) {
      return true;
    }

    if (this.isEdit) {
      /*
      if (this.name.value.toLowerCase() === this.currentName.toLowerCase()) {
        return true;
      }
      */
    }

    return false;
  }

  submit() {
    const formData = new FormData();
    formData.append('name', this.artistForm.value.name);
    formData.append('summary', this.artistForm.value.summary);
    formData.append('image', this.artistForm.get('image').value.files[0] as File);

    if (this.isEdit) {
      this.artistService.updateArtist(this.id, formData).subscribe((data: Artist) => {
        this.dialogRef.close(true);
      });
    } else {
      this.artistService.addArtist(formData).subscribe((data: Artist) => {
        if (data != null && data.id > 0) {
          this.dialogRef.close(true);
        }
      });
    }
  }
}
