using MusicManager.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Services.Communication
{
	public class AlbumResponse : BaseResponse
	{
		public Album Album { get; private set; }
		private AlbumResponse(bool success, string message, Album album) : base(success, message)
		{
			Album = album;
		}

		public AlbumResponse(Album album) : this(true, string.Empty, album)
		{ }

		public AlbumResponse(string message) : this(false, message, null)
		{ }
	}
}
