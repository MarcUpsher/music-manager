using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.Web.API.Domain.Models
{
	public class Genre
	{
		[Key]
		public int GenreId { get; set; }
		public string Name { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime? DateModified { get; set; }
		public DateTime? DateDeleted { get; set; }

		// References
		public List<AlbumGenre> AlbumGenres { get; set; }
	}
}
