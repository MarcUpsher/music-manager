using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.Web.API.Domain.Models
{
	public class Artist
	{
		public int ArtistId {get;set;}
		public int? ImageRefId { get; set; }
		public string Name { get; set; }
		public string Summary { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime? DateModified { get; set; }
		public DateTime? DateDeleted { get; set; }

		// References
		public List<Album> Albums { get; set; }
		public ImageRef ImageRef { get; set; }
	}
}
