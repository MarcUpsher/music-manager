using System;
using System.Collections.Generic;

namespace MusicManager.Web.API.DTO
{
	public class TrackDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Position { get; set; }
		public int Duration { get; set; }		
	}
}
