import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from './material.module';

import { LoaderComponent } from './components/loader/loader.component';
import { AppComponent } from './app.component';
import { AlbumListComponent } from './pages/albums/album-list/album-list.component';
import { AlbumViewComponent } from './pages/albums/album-view/album-view.component';
import { AlbumEditComponent } from './pages/albums/album-edit/album-edit.component';
import { AlbumCardComponent } from './components/album-card/album-card.component';

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
    LoaderComponent,
    AppComponent,
    AlbumListComponent,
    AlbumViewComponent,
    AlbumCardComponent,
    AlbumEditComponent,
    ConfirmDialogComponent,
    DurationPipe
  ],

  providers: [
    MatNativeDateModule
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
