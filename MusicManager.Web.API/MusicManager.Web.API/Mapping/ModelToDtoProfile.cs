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
		}
	}
}
