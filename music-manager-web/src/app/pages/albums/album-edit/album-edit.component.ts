import { Component, OnInit, Input, Inject, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlbumService } from '../album.service';
import { ArtistService } from '../../artists/artist.service';
import { Album, AlbumPost } from '../../../models/album';
import { FormBuilder, FormGroup, FormControl, FormArray, Validators } from '@angular/forms';
import { Track } from 'src/app/models/track';
import { FilterItem } from 'src/app/models/filter-item';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/components/confirm-dialog/confirm-dialog.component';
import { MatTable } from '@angular/material/table';
import { moveItemInArray } from '@angular/cdk/drag-drop';
import { CdkDragDrop } from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-album-edit',
  templateUrl: './album-edit.component.html',
  styleUrls: ['./album-edit.component.scss'],
  providers: []
})
export class AlbumEditComponent implements OnInit {
  loading = true;
  albumId: number;
  album: Album;
  albumForm: FormGroup;
  availableArtists: FilterItem[] = [];
  filteredArtists: Observable<FilterItem[]>;
  displayedColumns: string[] = ['drag', 'position', 'name', 'duration', 'actions'];

  @ViewChild(MatTable, { static: false }) table: MatTable<any>;

  constructor(
    public activatedRoute: ActivatedRoute,
    public router: Router,
    public albumService: AlbumService,
    public artistService: ArtistService,
    private fb: FormBuilder,
    public dialog: MatDialog
  ) {
    this.albumId = this.activatedRoute.snapshot.params.id;
  }

  ngOnInit() {
    this.albumForm = this.fb.group({
      name: ['', Validators.required],
      imageUri: [''],
      artist: ['', Validators.required],
      releaseDate: [''],
      summary: [''],
      genres: [''],
      tracks: this.fb.array([])
    });

    this.getAlbum(this.albumId);

    this.filteredArtists = this.albumForm.get('artist').valueChanges
      .pipe(
        startWith(''),
        map(value => typeof value === 'string' ? value : value.name),
        map(name => name ? this._filter(name) : this.availableArtists.slice())
      );
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

  getAlbum(id: number) {
    return this.albumService.getAlbum(id).subscribe((data: Album) => {
      this.album = data;

      for (const track of this.sortedTracks(data.tracks)) {
        this.tracks.push(this.newTrack(track));
      }

      this.albumForm.patchValue({
        name: data.name,
        artist: { id: data.artistId, name: data.artistName },
        imageUri: data.imageUri,
        releaseDate: data.releaseDate,
        summary: data.summary,
        genres: ''
      });

      this.artistService.getArtistsForFilter().subscribe((filterData: FilterItem[]) => {
        this.availableArtists = filterData;
      });

      this.loading = false;
    });
  }

  private _filter(name: any): FilterItem[] {
    const filterValue = name.toLowerCase();
    return this.availableArtists.filter(option => option.name.toLowerCase().indexOf(filterValue) === 0);
  }

  displayFn(item: FilterItem): string {
    return item && item.name ? item.name : '';
  }

  formSubmit() {
    const albumPost: AlbumPost = {
      id: this.albumId.toString(),
      name: this.albumForm.value.name,
      artistId: this.albumForm.value.artist.id.toString(),
      imageUri: this.albumForm.value.imageUri,
      summary: this.albumForm.value.summary,
      releaseDate: this.albumForm.value.releaseDate,
      tracks: this.albumForm.value.tracks,
      genres: []
    };

    console.info(albumPost);

    this.albumService.updateAlbum(this.albumId, albumPost).subscribe((data: Album) => {
      this.router.navigate(['album', this.albumId]);
    });

  }
}
