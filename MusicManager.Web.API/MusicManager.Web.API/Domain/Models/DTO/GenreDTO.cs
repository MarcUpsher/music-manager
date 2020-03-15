using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.Web.API.Domain.Models.DTO
{
	public class GenreDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public int AssociatedAlbums { get; set; }
	}

	public class GenrePostDTO
	{
		[Required]
		[MaxLength(300)]
		public string Name { get; set; }
	}
}
