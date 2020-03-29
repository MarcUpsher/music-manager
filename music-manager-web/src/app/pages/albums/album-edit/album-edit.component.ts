import { Component, OnInit, Input, Inject, ViewChild, Output, EventEmitter, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlbumService } from '../album.service';
import { ArtistService } from '../../artists/artist.service';
import { Album, AlbumPost } from '../../../models/album';
import { FormBuilder, FormGroup, FormArray, Validators, FormControl } from '@angular/forms';
import { Track } from 'src/app/models/track';
import { FilterItem } from 'src/app/models/filter-item';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { MatTable } from '@angular/material/table';
import { moveItemInArray } from '@angular/cdk/drag-drop';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { FileValidator } from 'ngx-material-file-input';
import { EventService } from '../../../shared/event.service';
import { Artist } from 'src/app/models/artist';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { MatAutocomplete, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { GenreService } from '../../genres/genre.service';
import { MatChipInputEvent } from '@angular/material/chips';

@Component({
  selector: 'app-album-edit',
  templateUrl: './album-edit.component.html',
  styleUrls: ['./album-edit.component.scss'],
  providers: []
})
export class AlbumEditComponent implements OnInit {
  @Input() artist: Artist;
  @Input() album: Album = null;
  @Output() closeAlbumView = new EventEmitter<any>();
  loading = true;
  albumForm: FormGroup;
  availableArtists: FilterItem[] = [];
  filteredArtists: Observable<FilterItem[]>;
  displayedColumns: string[] = ['drag', 'position', 'name', 'duration', 'actions'];
  imageURL = '';
  readonly maxSize = 1048576;
  isEdit = false;
  validating = false;

  visible = true;
  selectable = true;
  removable = true;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  genreCtrl = new FormControl();
  filteredGenres: Observable<string[]>;
  loadedGenres: string[] = [];
  availableGenres: string[] = [];
  allGenres: FilterItem[] = [];

  @ViewChild('genreInput') genreInput: ElementRef<HTMLInputElement>;
  @ViewChild('auto') matAutocomplete: MatAutocomplete;

  @ViewChild(MatTable, { static: false }) table: MatTable<any>;

  constructor(
    public activatedRoute: ActivatedRoute,
    public router: Router,
    public albumService: AlbumService,
    public artistService: ArtistService,
    public genreService: GenreService,
    public eventService: EventService,
    private fb: FormBuilder,
    public dialog: MatDialog
  ) {
  }

  ngOnInit() {
    this.albumForm = this.fb.group({
      name: ['', Validators.required],
      image: [undefined, [Validators.required, FileValidator.maxContentSize(this.maxSize)]],
      artist: [{ id: this.artist.id, name: this.artist.name }, Validators.required],
      releaseDate: [''],
      summary: [''],
      genres: this.fb.array([]),
      tracks: this.fb.array([])
    });

    if (this.album != null) {
      this.isEdit = true;
      this.getAlbum(this.album.id);
    }

    this.artistService.getArtistsForFilter().subscribe((filterData: FilterItem[]) => {
      this.availableArtists = filterData;
    });

    this.genreService.getGenresForFilter().subscribe((filterData: FilterItem[]) => {
      this.availableGenres = filterData.map(i => i.name);
      this.allGenres = filterData;
      this.loading = false;
    });

    this.filteredArtists = this.albumForm.get('artist').valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.name),
        map(name => name ? this._artistFilter(name) : this.availableArtists.slice())
      );

    this.filteredGenres = this.genreCtrl.valueChanges.pipe(
      startWith(null),
      map((genre: string | null) => genre ? this._genreFilter(genre) : this.availableGenres.slice()));
  }

  addGenre(event: MatChipInputEvent): void {
    const input = event.input;
    const value = event.value;

    if ((value || '').trim()) {
      this.loadedGenres.push(value.trim());
    }

    // Reset the input value
    if (input) {
      input.value = '';
    }

    this.genreCtrl.setValue(null);
  }

  removeGenre(genre: string): void {
    const index = this.loadedGenres.indexOf(genre);

    if (index >= 0) {
      this.loadedGenres.splice(index, 1);
    }
  }

  selectedGenre(event: MatAutocompleteSelectedEvent): void {
    this.loadedGenres.push(event.option.viewValue);
    this.genreInput.nativeElement.value = '';
    this.genreCtrl.setValue(null);
  }

  private _genreFilter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.availableGenres.filter(name => name.toLowerCase().indexOf(filterValue) === 0);
  }

  getAlbum(id: number) {
    return this.albumService.getAlbum(id).subscribe((data: Album) => {
      this.album = data;

      this.imageURL = data.imageUri;

      for (const track of this.sortedTracks(data.tracks)) {
        this.tracks.push(this.newTrack(track));
      }

      this.loadedGenres = data.genres;

      this.albumForm.patchValue({
        name: data.name,
        artist: { id: data.artistId, name: data.artistName },
        imageUri: data.imageUri,
        releaseDate: data.releaseDate,
        summary: data.summary
      });

      this.loading = false;
    });
  }

  private _artistFilter(name: any): FilterItem[] {
    const filterValue = name.toLowerCase();
    return this.availableArtists.filter(option => option.name.toLowerCase().indexOf(filterValue) === 0);
  }

  get name() {
    return this.albumForm.get('name');
  }

  get image() {
    return this.albumForm.get('image');
  }

  get summary() {
    return this.albumForm.get('summary');
  }

  get genres() {
    return this.albumForm.get('genres') as FormArray;
  }

  get tracks() {
    return this.albumForm.get('tracks') as FormArray;
  }

  newTrack(track: Track = null) {
    return this.fb.group({
      id: [track != null ? +track.id : null],
      name: [track != null ? track.name : ''],
      position: [track != null ? +track.position : null],
      duration: [track != null ? +track.duration : null]
    });
  }

  newGenre(genre: any = null) {
    return this.fb.group({
      id: [genre != null ? +genre.id : null],
      name: [genre != null ? genre.name : ''],
    });
  }

  addTrack() {
    this.tracks.push(this.newTrack());

    this.reorderTracks();
  }

  removeTrack(i: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '250px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.tracks.removeAt(i);

        this.reorderTracks();
      }
    });
  }

  onListDrop(event: CdkDragDrop<string[]>) {
    moveItemInArray(this.tracks.controls, event.previousIndex, event.currentIndex);

    this.reorderTracks();
  }

  sortedTracks(tracks: any) {
    return tracks.sort((a, b) => (a.position - b.position) ? 1 : -1);
  }

  reorderTracks() {
    let j = 1;
    for (const track of this.tracks.controls) {
      track.get('position').setValue(j);
      j++;
    }

    this.table.renderRows();
  }

  clearImage() {
    this.imageURL = '';
    this.albumForm.get('image').updateValueAndValidity();
  }

  onFileSelect(event: any) {
    const file = this.albumForm.get('image').value.files[0] as File;

    const reader = new FileReader();
    reader.onload = () => {
      this.imageURL = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  displayFn(item: FilterItem): string {
    return item && item.name ? item.name : '';
  }

  isFormInvalid() {
    if (this.loading || (!this.validating && !this.albumForm.valid)) {
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

  formSubmit() {
    const genreIds: any[] = [];

    this.loadedGenres.forEach(element => {
      const id = this.allGenres.find(i => i.name.toLowerCase() === element.toLowerCase()).id;
      if (genreIds.indexOf(id) === -1) {
        genreIds.push(id);
      }
    });

    const formData = new FormData();
    formData.append('name', this.albumForm.value.name);
    formData.append('artistId', this.albumForm.value.artist.id);
    formData.append('summary', this.albumForm.value.summary);
    formData.append('releaseDate', this.albumForm.value.releaseDate.toISOString().slice(0, 10));
    formData.append('image', this.albumForm.get('image').value.files[0] as File);
    formData.append('tracks', JSON.stringify(this.albumForm.value.tracks));
    formData.append('genres', JSON.stringify(genreIds));

    if (this.isEdit) {
      this.albumService.updateAlbum(this.album.id, formData).subscribe((data: Album) => {
        if (data != null && data.id > 0) {
          this.eventService.emitViewAlbumEvent(this.album);
        }
      });
    } else {
      this.albumService.addAlbum(formData).subscribe((data: Album) => {
        if (data != null && data.id > 0) {
          this.closeAlbumView.emit(true);
        }
      });
    }


    /*
    const albumPost: AlbumPost = {
      id: this.album.id.toString(),
      name: this.albumForm.value.name,
      artistId: this.albumForm.value.artist.id.toString(),
      imageUri: this.albumForm.value.imageUri,
      summary: this.albumForm.value.summary,
      releaseDate: this.albumForm.value.releaseDate,
      tracks: this.albumForm.value.tracks,
      genres: []
    };

    console.log(albumPost);

    this.albumService.updateAlbum(this.album.id, albumPost).subscribe((data: Album) => {
      this.backToAlbum();
    });
    */
  }

  backToAlbum() {
    this.eventService.emitViewAlbumEvent(this.album);
  }

  onCloseView() {
    this.closeAlbumView.emit(false);
  }

  /* Genres */


}
