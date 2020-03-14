using System;
using System.Collections.Generic;

namespace MusicManager.Web.API.Domain.Models.DTO
{
	public class TrackDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Position { get; set; }
		public string Duration { get; set; }		
	}
}
