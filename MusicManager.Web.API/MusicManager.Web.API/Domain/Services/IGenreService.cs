using MusicManager.Web.API.Domain.Models;
using MusicManager.Web.API.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Domain.Services
{
	public interface IGenreService
	{
    Task<IEnumerable<Genre>> ListAsync();
		Task<IEnumerable<Genre>> ListActiveAsync();
		Task<GenreResponse> SaveAsync(Genre genre);
		Task<GenreResponse> UpdateAsync(int id, Genre genre);
		Task<GenreResponse> DeleteAsync(int id);
		Task<GenreResponse> GetByIdAsync(int id);
		Task<Genre> GetByNameAsync(string name);
	}
}
