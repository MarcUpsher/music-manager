using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicManager.Web.API.Domain.Models
{
	public class Album
	{
		public int AlbumId { get; set; }
		public int ArtistId { get; set; }
		public int? ImageRefId { get; set; }
		public string Name { get; set; }
		public DateTime ReleaseDate  { get; set; }
		public string Summary { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime? DateModified { get; set; }
		public DateTime? DateDeleted { get; set; }

		//References
		public Artist Artist { get; set; }
		public List<Track> Tracks { get; set; }
		public ImageRef ImageRef { get; set; }
		public List<AlbumGenre> AlbumGenres { get; set; }
	}
}
