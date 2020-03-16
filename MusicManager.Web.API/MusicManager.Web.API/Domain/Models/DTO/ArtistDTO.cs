using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MusicManager.Web.API.Domain.Models.DTO
{
	public class ArtistDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Summary { get; set; }
		public string ImageUriId { get; set; }
		public string ImageUri { get; set; }
		public int NumberOfAlbums { get; set; }
		public List<AlbumDTO> Albums { get; set; }
	}

	public class ArtistPostDTO
	{		
		public string Name { get; set; }
		public string Summary { get; set; }
		public string ImageUriId { get; set; }
		public IFormFile Image { get; set; }
	}
}
