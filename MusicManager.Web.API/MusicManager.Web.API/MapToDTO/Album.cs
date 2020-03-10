using MusicManager.DAL;
using MusicManager.Web.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.MapToDTO
{
	public static class AlbumMap
	{
		public static List<AlbumDTO> ListToDTO(List<Album> albums)
		{
			List<AlbumDTO> albumsDTO = albums.Select(album => ToDTO(album)).ToList();

			return albumsDTO;
		}

		public static AlbumDTO ToDTO(Album album)
		{
			return new AlbumDTO()
			{
				Id = album.AlbumId,
				ArtistId = album.Artist != null ? album.Artist.ArtistId : 0,
				ArtistName = album.Artist != null ? album.Artist.Name : "Unknown",
				Name = album.Name,
				Summary = album.Summary,
				ReleaseDate = album.ReleaseDate,
				Genres = album.AlbumGenres != null ? album.AlbumGenres.Select(s => s.Genre.Name).ToList() : new List<string>(),
				DateAdded = album.DateAdded,
				ImageUri = album.ImageRef != null ? Helpers.GetImageUri(album.ImageRef.URI) : "",
				NumberOfTracks = album.Tracks != null ? album.Tracks.Count() : 0,
				Tracks = TrackMap.ListToDTO(album.Tracks)
			};
		}
	}
}
