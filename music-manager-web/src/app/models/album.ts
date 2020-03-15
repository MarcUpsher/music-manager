import { Track } from './track';

export class Album {
  id: number;
  name: string;
  imageUri: string;
  summary: string;
  releaseDate: Date;
  numberOfTracks: string;
  tracks: Track[];
  genres: string[];
}

export class AlbumPost {
  id: string;
  name: string;
  artistId: string;
  imageUri: string;
  summary: string;
  releaseDate: Date;
  tracks: Track[];
  genres: string[];
}
