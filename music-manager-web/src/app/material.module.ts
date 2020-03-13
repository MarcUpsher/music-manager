import { NgModule } from '@angular/core';

import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

const materialModules = [
  MatTableModule,
  MatDialogModule,
  MatToolbarModule,
  MatGridListModule,
  MatCardModule,
  MatProgressSpinnerModule,
  MatButtonModule,
  MatInputModule,
  MatAutocompleteModule,
  DragDropModule,
  MatDatepickerModule,
  MatNativeDateModule
];

@NgModule({
  imports: materialModules,
  exports: materialModules
})
export class MaterialModule {}
