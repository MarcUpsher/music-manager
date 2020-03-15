using MusicManager.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Services.Communication
{
	public class ArtistResponse : BaseResponse
	{
		public Artist Artist { get; private set; }
		private ArtistResponse(bool success, string message, Artist artist) : base(success, message)
		{
			Artist = artist;
		}

		public ArtistResponse(Artist artist) : this(true, string.Empty, artist)
		{ }

		public ArtistResponse(string message) : this(false, message, null)
		{ }
	}
}
