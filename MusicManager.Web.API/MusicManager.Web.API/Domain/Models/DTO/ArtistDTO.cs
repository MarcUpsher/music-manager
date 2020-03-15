using System;
using System.Collections.Generic;

namespace MusicManager.Web.API.Domain.Models.DTO
{
	public class BaseArtistDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Summary { get; set; }
		public string ImageUri { get; set; }
	}
	public class ArtistDTO : BaseArtistDTO
	{
		public int NumberOfAlbums { get; set; }
	}

	public class ArtistWithAlbumsDTO : BaseArtistDTO
	{
		public List<AlbumDTO> Albums { get; set; }
	}
}
