import { Album } from './album';

export class BaseArtist {
  id: number;
  name: string;
  summary: string;
  imageUri: string;
}

export class Artist extends BaseArtist {
  numberOfAlbums: number;
}

export class ArtistWithAlbums extends BaseArtist {
  albums: Album[];
}

export class ArtistPostDTO {
  name: string;
  summary: string;
  image: File;

  constructor(name = '', summmary = '', image: any) {
    this.name = name;
    this.summary = summmary;
    this.image = image;
  }
}
