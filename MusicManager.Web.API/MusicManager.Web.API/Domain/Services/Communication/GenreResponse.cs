using MusicManager.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Services.Communication
{
	public class GenreResponse : BaseResponse
	{
		public Genre Genre { get; private set; }
		private GenreResponse(bool success, string message, Genre genre) : base(success, message)
		{
			Genre = genre;
		}

		public GenreResponse(Genre genre) : this(true, string.Empty, genre)
		{ }

		public GenreResponse(string message) : this(false, message, null)
		{ }
	}
}
