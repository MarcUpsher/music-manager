using System;
using System.Collections.Generic;

namespace MusicManager.Web.API.DTO
{
	public class AlbumDTO
	{
		public int Id { get; set; }			
		public string Name { get; set; }
		public string ArtistName { get; set; }
		public string ImageUri { get; set; }
		public int NumberOfTracks { get; set; }
		public List<string> Genres { get; set; }
		public DateTime DateAdded { get; set; }
	}
}
