using MusicManager.DAL;
using MusicManager.Web.API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.MapToDTO
{
	public static class TrackMap
	{
		public static List<TrackDTO> ListToDTO(List<Track> tracks)
		{
			return tracks.Select(s => ToDTO(s)).ToList();
		}

		public static TrackDTO ToDTO(Track track)
		{
			return new TrackDTO()
			{
				Id = track.TrackId.ToString(),
				Name = track.Name,
				Position = track.Position.ToString(),
				Duration = track.Duration.ToString()
			};
		}
	}
}
