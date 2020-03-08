using System;

namespace MusicManager.Web.API.DTO
{
	public class AlbumDTO
	{
		public int AlbumId { get; set; }
		public int ArtistId { get; set; }		
		public string Name { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime DateModified { get; set; }
		public DateTime DateDeleted { get; set; }
	}
}
