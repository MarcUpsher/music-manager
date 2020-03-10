import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AlbumListComponent } from './pages/albums/album-list/album-list.component';
import { AlbumViewComponent } from './pages/albums/album-view/album-view.component';

const routes: Routes = [
  { path: '', redirectTo: '/albums', pathMatch: 'full' },
  { path: 'albums', component: AlbumListComponent },
  { path: 'album/:id', component: AlbumViewComponent}
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
