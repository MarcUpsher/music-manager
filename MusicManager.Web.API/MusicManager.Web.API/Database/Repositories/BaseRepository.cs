using MusicManager.Web.API.Database.Contexts;

namespace MusicManager.Web.API.Database.Repositories
{
	public abstract class BaseRepository
	{
		protected readonly MusicManagerContext _context;

		public BaseRepository(MusicManagerContext context)
		{
			_context = context;
		}
	}
}
