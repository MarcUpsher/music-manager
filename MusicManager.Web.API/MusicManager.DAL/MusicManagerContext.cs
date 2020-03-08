using Microsoft.EntityFrameworkCore;
using MusicManager.DAL.Configuration;
using System;

namespace MusicManager.DAL
{
	public class MusicManagerContext : DbContext
	{
		public MusicManagerContext(DbContextOptions<MusicManagerContext> options)
			: base(options)
		{ }

		public DbSet<Artist> Artists { get; set; }
		public DbSet<Album> Albums { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<AlbumGenre> AlbumGenres { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new GenreConfiguration());
			modelBuilder.ApplyConfiguration(new ArtistConfiguration());
			modelBuilder.ApplyConfiguration(new AlbumConfiguration());
			modelBuilder.ApplyConfiguration(new TrackConfiguration());
			modelBuilder.ApplyConfiguration(new AlbumGenreConfiguration());

			// Seed data for initial migration

			modelBuilder.Entity<Genre>().HasData(
				new Genre { GenreId = 1, Name = "Rock" },
				new Genre { GenreId = 2, Name = "Metal" },
				new Genre { GenreId = 3, Name = "Pop" },
				new Genre { GenreId = 4, Name = "Symphonic" });

			modelBuilder.Entity<Artist>().HasData(
				new Artist { ArtistId = 1, Name = "The Night Flight Orchestra" },
				new Artist { ArtistId = 2, Name = "Abba" },
				new Artist { ArtistId = 3, Name = "Bruce Springsteen" });

			modelBuilder.Entity<Album>().HasData(
				new Album
				{
					AlbumId = 1,
					ArtistId = 1,
					Name = "Internal Affairs",
					ReleaseDate = DateTime.Parse("2012-06-18"),
					Summary = "Internal Affairs is the first studio album by Swedish classic rock/AOR band The Night Flight Orchestra, released on 18 June 2012 via Coroner Records."
				});

			modelBuilder.Entity<AlbumGenre>().HasData(
				new AlbumGenre { AlbumId = 1, GenreId = 1 },
				new AlbumGenre { AlbumId = 1, GenreId = 3 }
				);

			modelBuilder.Entity<Track>().HasData(
				new Track { TrackId = 1, AlbumId = 1, Position = 1, Name = "Siberian Queen", Duration = 363 },
				new Track { TrackId = 2, AlbumId = 1, Position = 2, Name = "California Morning", Duration = 337 },
				new Track { TrackId = 3, AlbumId = 1, Position = 3, Name = "Glowing City Madness", Duration = 237 },
				new Track { TrackId = 4, AlbumId = 1, Position = 4, Name = "West Ruth Ave", Duration = 402 },
				new Track { TrackId = 5, AlbumId = 1, Position = 5, Name = "Transatlantic Blues", Duration = 501 },
				new Track { TrackId = 6, AlbumId = 1, Position = 6, Name = "Miami 5:02", Duration = 229 },
				new Track { TrackId = 7, AlbumId = 1, Position = 7, Name = "Internal Affairs", Duration = 298 },
				new Track { TrackId = 8, AlbumId = 1, Position = 8, Name = "1998", Duration = 297 },
				new Track { TrackId = 9, AlbumId = 1, Position = 9, Name = "Stella Ain't No Dove", Duration = 267 },
				new Track { TrackId = 10, AlbumId = 1, Position = 10, Name = "Montreal Midnight Supply", Duration = 281 },
				new Track { TrackId = 11, AlbumId = 1, Position = 11, Name = "Green Hills of Glumslöv", Duration = 248 }
				);
		}
	}
}