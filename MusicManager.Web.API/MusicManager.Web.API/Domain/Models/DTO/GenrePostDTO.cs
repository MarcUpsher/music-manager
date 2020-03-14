using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MusicManager.Web.API.Domain.Models.DTO
{
	public class GenrePostDTO
	{
		[Required]
		[MaxLength(300)]
		public string Name { get; set; }
	}
}
