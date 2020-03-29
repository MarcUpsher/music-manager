using AutoMapper;
using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Models.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Mapping
{
	public class DtoToModelProfile : Profile
	{
		public DtoToModelProfile()
		{
			CreateMap<GenrePostDTO, Genre>();

			CreateMap<AlbumGenreDTO, AlbumGenre>()
				.ForMember(d => d.AlbumGenreId, o => o.MapFrom(s => -1))
				.ForMember(d => d.AlbumId, o => o.MapFrom(s => -1))
				.ForMember(d => d.GenreId, o => o.MapFrom(s => s.Id));


			CreateMap<AlbumPostDTO, Album>()
				.ForMember(d => d.AlbumId, o => o.MapFrom(s => s.Id))
				.ForMember(d => d.ReleaseDate, o => o.MapFrom(s => DateTime.ParseExact(s.ReleaseDate + " 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)))
				.ForMember(d => d.AlbumGenres, o => o.MapFrom(s => new List<AlbumGenre>()))
				.ForMember(d => d.Tracks, o => o.MapFrom(s => new List<Track>()));

			CreateMap<ArtistPostDTO, Artist>();

			CreateMap<TrackDTO, Track>()
				.ForMember(d => d.TrackId, o => o.MapFrom(s => s.Id));
		}
	}
}
