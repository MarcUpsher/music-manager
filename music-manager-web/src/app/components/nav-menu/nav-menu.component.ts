import { Component, OnInit } from '@angular/core';

export class LinkSection {
  constructor(public name: string, public links: Link[]) {}
}

export class Link {
  constructor(public name: string, public href: string) {}
}

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss'],
  providers: []
})
export class NavMenuComponent implements OnInit {
  sections: LinkSection[] = [];

  constructor(
  ) {
  }

  ngOnInit() {
    this.sections.push(new LinkSection('Artists', [new Link('List', '/artist/list')]));
    this.sections.push(new LinkSection('Genres', [new Link('List', '/genre/list')]));
  }
}
