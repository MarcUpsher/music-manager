import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss'],
  providers: []
})
export class PageHeaderComponent implements OnInit {
  @Input() title: string;

  constructor(
  ) {
  }

  ngOnInit() {
  }
}
