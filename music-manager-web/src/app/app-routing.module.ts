import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AlbumListComponent } from './pages/albums/album-list/album-list.component';
import { AlbumViewComponent } from './pages/albums/album-view/album-view.component';
import { AlbumEditComponent } from './pages/albums/album-edit/album-edit.component';

import { ArtistListComponent } from './pages/artists/artist-list/artist-list.component';
import { ArtistViewComponent } from './pages/artists/artist-view/artist-view.component';
import { ArtistEditComponent } from './pages/artists/artist-edit/artist-edit.component';

import { TrackListComponent } from './pages/tracks/track-list/track-list.component';
import { TrackViewComponent } from './pages/tracks/track-view/track-view.component';
import { TrackEditComponent } from './pages/tracks/track-edit/track-edit.component';

import { GenreListComponent } from './pages/genres/genre-list/genre-list.component';
import { GenreViewComponent } from './pages/genres/genre-view/genre-view.component';
import { GenreEditComponent } from './pages/genres/genre-edit/genre-edit.component';

const routes: Routes = [
  { path: '', redirectTo: '/albums', pathMatch: 'full' },

  { path: 'genre/list', component: GenreListComponent },
  { path: 'genre/maintain', component: GenreEditComponent },

  { path: 'artist/list', component: ArtistListComponent },
  { path: 'artist/:id', component: ArtistViewComponent},
  { path: 'artist/:id/edit', component: ArtistEditComponent},
  { path: 'artist/add', component: ArtistEditComponent},

  { path: 'album/list', component: AlbumListComponent },
  { path: 'album/:id', component: AlbumViewComponent},
  { path: 'album/:id/edit', component: AlbumEditComponent},
  { path: 'album/add', component: AlbumEditComponent},

  { path: 'track/list', component: TrackListComponent },
  { path: 'album/:id/tracks', component: TrackListComponent},
  { path: 'album/:id/tracks/edit', component: TrackEditComponent}
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      routes,
      { enableTracing: true } // <-- debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
