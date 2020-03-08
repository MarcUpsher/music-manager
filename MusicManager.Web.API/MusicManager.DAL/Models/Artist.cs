using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.DAL
{
	public class Artist
	{
		public int ArtistId {get;set;}
		public string Name { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime? DateModified { get; set; }
		public DateTime? DateDeleted { get; set; }

		// References
		public virtual List<Album> Albums { get; set; }
	}
}
