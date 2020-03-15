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
