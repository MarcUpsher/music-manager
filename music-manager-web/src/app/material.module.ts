import { NgModule } from '@angular/core';

import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import {MatToolbarModule} from '@angular/material/toolbar';

@NgModule({
  exports: [
    MatTableModule,
    MatDialogModule,
    MatToolbarModule]
})
export class MaterialModule {}
