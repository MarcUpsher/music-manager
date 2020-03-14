using MusicManager.Web.API.Database.Contexts;
using MusicManager.Web.API.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Database.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MusicManagerContext _context;

		public UnitOfWork(MusicManagerContext context)
		{
			_context = context;
		}

		public async Task CompleteAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
