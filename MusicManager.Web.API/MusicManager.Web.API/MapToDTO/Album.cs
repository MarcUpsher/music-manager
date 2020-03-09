using MusicManager.DAL;
using MusicManager.Web.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.MapToDTO
{
	public class AlbumMap
	{
		public static List<AlbumDTO> ToDTO(List<Album> albums)
		{
			List<AlbumDTO> albumsDTO = albums.Select(s => new AlbumDTO()
			{
				Id = s.AlbumId,
				ArtistName = s.Artist != null ? s.Artist.Name : "Unknown",
				Name = s.Name,
				Genres = s.AlbumGenres != null ? s.AlbumGenres.Select(s => s.Genre.Name).ToList() : new List<string>(),
				DateAdded = s.DateAdded,
				ImageUri = s.ImageRef != null ? s.ImageRef.URI : "",
				NumberOfTracks = s.Tracks != null ? s.Tracks.Count() : 0
			}).ToList();

			return albumsDTO;
		}

		public static List<Album> FromDTO(List<AlbumDTO> albumsDTO)
		{
			List<Album> albums = albumsDTO.Select(s => new Album()
			{
				AlbumId = s.Id,
				Name = s.Name
			}).ToList();

			return albums;
		}
	}
}
