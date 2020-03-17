﻿using AutoMapper;
using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Models.DTO;
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

			CreateMap<AlbumPostDTO, Album>()
				.ForMember(d => d.ReleaseDate, o => o.MapFrom(s => DateTime.ParseExact(s.ReleaseDate + " 00:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)));

			CreateMap<ArtistPostDTO, Artist>();
			CreateMap<TrackDTO, Track>();
		}
	}
}
