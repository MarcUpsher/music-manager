using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.DAL
{
	public class Track
	{
		[Key]
		public int TrackId { get; set; }
		public int AlbumId { get; set; }
		public int Position { get; set; }
		public int Duration { get; set; }
		public string Name { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime? DateModified { get; set; }
		public DateTime? DateDeleted { get; set; }

		// References
		public Album Album { get; set; }
	}
}
