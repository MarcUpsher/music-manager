using System;
using System.Collections.Generic;

namespace MusicManager.Web.API.Domain.Models.DTO
{
	public class GenreDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int AssociatedAlbums { get; set; }
	}
}
