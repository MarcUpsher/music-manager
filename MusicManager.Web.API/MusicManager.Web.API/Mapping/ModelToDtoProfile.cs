using AutoMapper;
using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Mapping
{
	public class ModelToDtoProfile : Profile
	{
		public ModelToDtoProfile()
		{
			CreateMap<Genre, GenreDTO>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.GenreId))
				.ForMember(d => d.AssociatedAlbums, o => o.MapFrom(s => s.AlbumGenres.Count()));

			CreateMap<Track, TrackDTO>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.TrackId));

			CreateMap<AlbumGenre, AlbumGenreDTO>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.AlbumGenreId))
				.ForMember(d => d.Name, o => o.MapFrom(s => s.Genre.Name));

			CreateMap<Album, AlbumDTO>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.AlbumId))
				.ForMember(d => d.ImageUri, o => o.MapFrom(s => s.ImageRef.URI))
				.ForMember(d => d.NumberOfTracks, o => o.MapFrom(s => s.Tracks.Count()))
				.ForMember(d => d.Genres, o => o.MapFrom(s => s.AlbumGenres.Select(s => s.Genre.Name)));

			CreateMap<Artist, ArtistDTO>()
				.ForMember(d => d.Id, o => o.MapFrom(s => s.ArtistId))
				.ForMember(d => d.ImageUri, o => o.MapFrom(s => s.ImageRef.URI))
				.ForMember(d => d.NumberOfAlbums, o => o.MapFrom(s => s.Albums.Count()));
		}
	}
}
