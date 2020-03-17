import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AlbumListComponent } from './pages/albums/album-list/album-list.component';
import { AlbumViewComponent } from './pages/albums/album-view/album-view.component';
import { AlbumEditComponent } from './pages/albums/album-edit/album-edit.component';

import { ArtistListComponent } from './pages/artists/artist-list/artist-list.component';
import { ArtistViewComponent } from './pages/artists/artist-view/artist-view.component';
import { GenreListComponent } from './pages/genres/genre-list/genre-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'artist/list', pathMatch: 'full' },

  { path: 'genre/list', component: GenreListComponent },

  { path: 'artist/list', component: ArtistListComponent },
  { path: 'artist/:artistId', component: ArtistViewComponent},
  { path: 'artist/:artistId/album/:albumId', component: ArtistViewComponent},
  { path: 'artist/:artistId/album/:albumId/edit', component: ArtistViewComponent},
  { path: 'artist/:artistId/add-album', component: ArtistViewComponent},

  { path: 'album/list', component: AlbumListComponent },
  { path: 'album/:id', component: AlbumViewComponent},
  { path: 'album/:id/edit', component: AlbumEditComponent},
  { path: 'album/add', component: AlbumEditComponent},
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
