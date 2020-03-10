import { Track } from './track';

export class Album {
  id: number;
  name: string;
  artistId: number;
  artistName: string;
  imageUri: string;
  summary: string;
  releaseDate: Date;
  numberOfTracks: string;
  tracks: Track[];
  genres: string[];
  dateAdded: Date;
}
