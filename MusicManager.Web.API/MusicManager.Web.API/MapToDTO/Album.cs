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
				AlbumId = s.AlbumId,
				ArtistId = s.ArtistId,
				Name = s.Name,
				DateAdded = s.DateAdded
			}).ToList();

			return albumsDTO;
		}

		public static List<Album> FromDTO(List<AlbumDTO> albumsDTO)
		{
			List<Album> albums = albumsDTO.Select(s => new Album()
			{
				AlbumId = s.AlbumId,
				ArtistId = s.ArtistId,
				Name = s.Name,
				DateAdded = s.DateAdded
			}).ToList();

			return albums;
		}
	}
}
