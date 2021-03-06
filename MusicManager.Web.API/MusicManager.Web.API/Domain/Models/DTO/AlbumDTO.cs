using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MusicManager.Web.API.Domain.Models.DTO
{
	public class AlbumDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ArtistId { get; set; }
		public string ArtistName { get; set; }
		public string Summary { get; set; }
		public DateTime ReleaseDate { get; set; }
		public string ImageUri { get; set; }
		public int NumberOfTracks { get; set; }
		public List<string> Genres { get; set; }
		public List<TrackDTO> Tracks { get; set; }
	}

	public class AlbumPostDTO
	{
		public string Id { get; set; }		
		public string Name { get; set; }
		public string ArtistId { get; set; }
		public string Summary { get; set; }
		public string ReleaseDate { get; set; }
		public string Genres { get; set; }
		public string Tracks { get; set; }
		public IFormFile Image { get; set; }
	}
}
