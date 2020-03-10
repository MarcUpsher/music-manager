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
import { AlbumCardComponent } from './components/album-card/album-card.component';

import { DurationPipe } from './pipes/duration.pipe';

@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MaterialModule
  ],
  declarations: [
    LoaderComponent,
    AppComponent,
    AlbumListComponent,
    AlbumViewComponent,
    AlbumCardComponent,
    DurationPipe
  ],

  providers: [],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
