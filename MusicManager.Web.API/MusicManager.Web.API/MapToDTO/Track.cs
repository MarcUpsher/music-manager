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
			List<TrackDTO> trackDTO = tracks.Select(s => ToDTO(s)).ToList();

			return trackDTO;
		}

		public static TrackDTO ToDTO(Track track)
		{
			return new TrackDTO()
			{
				Id = track.TrackId,
				Name = track.Name,
				Position = track.Position,
				Duration = track.Duration
			};
		}
	}
}
