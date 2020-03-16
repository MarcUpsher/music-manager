using MusicManager.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Services.Communication
{
	public class ImageRefResponse : BaseResponse
	{
		public ImageRef ImageRef { get; private set; }
		private ImageRefResponse(bool success, string message, ImageRef imageRef) : base(success, message)
		{
			ImageRef = imageRef;
		}

		public ImageRefResponse(ImageRef imageRef) : this(true, string.Empty, imageRef)
		{ }

		public ImageRefResponse(string message) : this(false, message, null)
		{ }
	}
}
