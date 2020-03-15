export class Genre {
  id: number;
  name: string;
  associatedAlbums: number;
}

export class GenrePostDTO {
  name: string;

  constructor(name = '') {
    this.name = name;
  }
}
