using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.Web.API.Domain.Models
{
	public class AlbumGenre
	{
		public int AlbumGenreId { get; set; }
		public int AlbumId { get; set; }		
		public int GenreId { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime? DateDeleted { get; set; }

		// References
		public Album Album { get; set; }
		public Genre Genre { get; set; }
	}
}
