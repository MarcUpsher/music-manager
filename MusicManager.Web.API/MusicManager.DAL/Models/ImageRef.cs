using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.DAL
{
	public class ImageRef
	{		
		public int ImageRefId { get; set; }
		public string URI { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime? DateDeleted { get; set; }

		// References
		public List<Artist> Artists { get; set; }
		public List<Album> Albums { get; set; }
	}
}
