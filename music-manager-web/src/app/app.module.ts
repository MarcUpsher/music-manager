import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA  } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';

import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { LoaderComponent } from './components/loader/loader.component';
import { PageHeaderComponent } from './components/page-header/page-header.component';
import { AppComponent } from './app.component';

import { AlbumListComponent } from './pages/albums/album-list/album-list.component';
import { AlbumViewComponent } from './pages/albums/album-view/album-view.component';
import { AlbumEditComponent } from './pages/albums/album-edit/album-edit.component';
import { AlbumCardComponent } from './components/album-card/album-card.component';

import { ArtistListComponent } from './pages/artists/artist-list/artist-list.component';
import { ArtistViewComponent } from './pages/artists/artist-view/artist-view.component';
import { ArtistEditComponent } from './pages/artists/artist-edit/artist-edit.component';
import { ArtistCardComponent } from './components/artist-card/artist-card.component';

import { TrackListComponent } from './pages/tracks/track-list/track-list.component';
import { TrackViewComponent } from './pages/tracks/track-view/track-view.component';
import { TrackEditComponent } from './pages/tracks/track-edit/track-edit.component';

import { GenreListComponent } from './pages/genres/genre-list/genre-list.component';
import { GenreEditComponent } from './pages/genres/genre-edit/genre-edit.component';

import { ConfirmDialogComponent } from './components/confirm-dialog/confirm-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DurationPipe } from './pipes/duration.pipe';
import { MatNativeDateModule } from '@angular/material/core';

@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    ReactiveFormsModule,
    MaterialModule
  ],
  declarations: [
    NavMenuComponent,
    LoaderComponent,
    PageHeaderComponent,
    AppComponent,
    AlbumListComponent,
    AlbumViewComponent,
    AlbumCardComponent,
    AlbumEditComponent,
    ArtistListComponent,
    ArtistViewComponent,
    ArtistEditComponent,
    ArtistCardComponent,
    TrackListComponent,
    TrackViewComponent,
    TrackEditComponent,
    GenreListComponent,
    GenreEditComponent,
    ConfirmDialogComponent,
    DurationPipe
  ],

  providers: [
    MatNativeDateModule
  ],
  bootstrap: [
    AppComponent
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class AppModule { }
