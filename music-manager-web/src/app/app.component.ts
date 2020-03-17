import { Component, ViewEncapsulation } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  opened = false;
  title = 'Music Manager';

  constructor() {

  }

  openSidenav() {
    this.opened = true;
  }
}
