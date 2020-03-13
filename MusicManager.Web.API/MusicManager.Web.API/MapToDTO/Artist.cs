using MusicManager.DAL;
using MusicManager.Web.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.MapToDTO
{
	public static class ArtistMap
	{
		public static List<ArtistDTO> ListToDTO(List<Artist> artists)
		{
			return artists.Select(artist => ToDTO(artist)).ToList();
		}

		public static ArtistDTO ToDTO(Artist artist)
		{
			return new ArtistDTO()
			{
				Id = artist.ArtistId
			};
		}

		public static List<FilterItemDTO> ListToFilterItemDTO(List<Artist> artists)
		{
			return artists.Select(artist => ToFilterItemDTO(artist)).ToList();
		}
		public static FilterItemDTO ToFilterItemDTO(Artist artist)
		{
			return new FilterItemDTO()
			{
				Id = artist.ArtistId,
				Name = artist.Name
			};
		}
	}
}
