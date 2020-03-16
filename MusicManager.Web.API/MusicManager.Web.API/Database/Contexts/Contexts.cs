using System;
using Microsoft.EntityFrameworkCore;
using MusicManager.DAL.Configuration;
using MusicManager.Web.API.Domain.Models;


namespace MusicManager.Web.API.Database.Contexts
{
	public class MusicManagerContext : DbContext
	{
		public DbSet<Artist> Artists { get; set; }
		public DbSet<Album> Albums { get; set; }
		public DbSet<Genre> Genres { get; set; }
		public DbSet<Track> Tracks { get; set; }
		public DbSet<AlbumGenre> AlbumGenres { get; set; }
		public DbSet<ImageRef> ImageRefs { get; set; }

		public MusicManagerContext(DbContextOptions<MusicManagerContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new ImageRefConfiguration());
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
				new Genre { GenreId = 4, Name = "Symphonic" },
				new Genre { GenreId = 5, Name = "Progressive" },
				new Genre { GenreId = 6, Name = "Acoustic" },
				new Genre { GenreId = 7, Name = "Folk" }
				);

			modelBuilder.Entity<ImageRef>().HasData(
				new ImageRef { ImageRefId = 1, URI = "images/20200309_Born_to_Run_(Front_Cover).jpg" },
				new ImageRef { ImageRefId = 2, URI = "images/20200309_Bruce_Springsteen_-_Nebraska.jpg" },
				new ImageRef { ImageRefId = 3, URI = "images/20200309_bruce-springsteen-260-260.jpg" },
				new ImageRef { ImageRefId = 4, URI = "images/20200309_In_the_Passing_Light_of_Day_Cover.jpg" },
				new ImageRef { ImageRefId = 5, URI = "images/20200309_NFO.jpg" },
				new ImageRef { ImageRefId = 6, URI = "images/20200309_PainOfSalvation.jpg" },
				new ImageRef { ImageRefId = 7, URI = "images/20200309-NFO_Internal_Affairs.jpg" }
			);

			modelBuilder.Entity<Artist>().HasData(
				new Artist
				{
					ArtistId = 1,
					ImageRefId = 5,
					Name = "The Night Flight Orchestra",
					Summary = "The Night Flight Orchestra is a Swedish hard rock band from Helsingborg. They are signed to Nuclear Blast Records. Formed in 2007 by Björn Strid and David Andersson while they were touring in the US with their band Soilwork, they were later joined by bassist Sharlee D'Angelo (Arch Enemy, Spiritual Beggars, ex-King Diamond), Richard Larsson (Von Benzo), Jonas Källsbäck (Mean Streak) and, more recently, Sebastian Forslund (Kadwatha). They have released six studio albums."
				},
				new Artist
				{
					ArtistId = 2,
					ImageRefId = 6,
					Name = "Pain Of Salvation",
					Summary = "Pain of Salvation is a Swedish progressive metal band led by Daniel Gildenlöw, who is the band's main songwriter, lyricist, guitarist, and vocalist. Pain of Salvation's sound is characterised by riff-oriented guitar work, a broad vocal range, oscillation between heavy and calm passages, complex vocal harmonies and the structures of their albums, syncopation, and polyrhythms. Thus far, every album released by the band has been a concept album. Lyrically, the band tends to address contemporary issues, such as sexuality, war, the environment, and the nature of God, humanity, and existence."
				},
				new Artist
				{
					ArtistId = 3,
					ImageRefId = 3,
					Name = "Bruce Springsteen",
					Summary = "Bruce Springsteen, born September 23, 1949, is an American singer, songwriter, and musician who is both a solo artist and the leader of the E Street Band. He received critical acclaim for his early 1970s albums and attained worldwide fame upon the release of Born to Run in 1975. During a career that has spanned five decades, Springsteen has become known for his poetic and socially conscious lyrics and lengthy, energetic stage performances, earning the nickname \"The Boss\". He has recorded both rock albums and folk-oriented works, and his lyrics often address the experiences and struggles of working-class Americans."
				}
				);

			modelBuilder.Entity<Album>().HasData(
				new Album
				{
					AlbumId = 1,
					ArtistId = 1,
					ImageRefId = 7,
					Name = "Internal Affairs",
					ReleaseDate = System.DateTime.Parse("2012-06-18"),
					Summary = "Internal Affairs is the first studio album by Swedish classic rock/AOR band The Night Flight Orchestra, released on 18 June 2012 via Coroner Records."
				},
				new Album
				{
					AlbumId = 2,
					ArtistId = 3,
					ImageRefId = 2,
					Name = "Nebraska",
					ReleaseDate = DateTime.Parse("1982-12-30"),
					Summary = "Nebraska is the sixth studio album by Bruce Springsteen, released on September 30, 1982, by Columbia Records."
				},
				new Album
				{
					AlbumId = 3,
					ArtistId = 2,
					ImageRefId = 4,
					Name = "In The Passing Light Of Day",
					ReleaseDate = DateTime.Parse("2017-01-13"),
					Summary = "In the Passing Light of Day is the tenth studio album by Swedish band Pain of Salvation and was released on 13 January 2017 by InsideOut."
				},
				new Album
				{
					AlbumId = 4,
					ArtistId = 3,
					ImageRefId = 1,
					Name = "Born To Run",
					ReleaseDate = DateTime.Parse("1975-08-25"),
					Summary = "Born to Run is the third studio album by American singer-songwriter Bruce Springsteen."
				}
			);

			modelBuilder.Entity<AlbumGenre>().HasData(
				new AlbumGenre { AlbumGenreId = 1, AlbumId = 1, GenreId = 1 },
				new AlbumGenre { AlbumGenreId = 2, AlbumId = 1, GenreId = 3 },
				new AlbumGenre { AlbumGenreId = 3, AlbumId = 2, GenreId = 6 },
				new AlbumGenre { AlbumGenreId = 4, AlbumId = 2, GenreId = 7 },
				new AlbumGenre { AlbumGenreId = 5, AlbumId = 3, GenreId = 1 },
				new AlbumGenre { AlbumGenreId = 6, AlbumId = 3, GenreId = 2 },
				new AlbumGenre { AlbumGenreId = 7, AlbumId = 3, GenreId = 5 },
				new AlbumGenre { AlbumGenreId = 8, AlbumId = 4, GenreId = 1 }
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
				new Track { TrackId = 11, AlbumId = 1, Position = 11, Name = "Green Hills of Glumslöv", Duration = 248 },

				new Track { TrackId = 12, AlbumId = 2, Position = 1, Name = "Nebraska", Duration = 272 },
				new Track { TrackId = 13, AlbumId = 2, Position = 2, Name = "Atlantic City", Duration = 240 },
				new Track { TrackId = 14, AlbumId = 2, Position = 3, Name = "Mansion on the Hill", Duration = 248 },
				new Track { TrackId = 15, AlbumId = 2, Position = 4, Name = "Johnny 99", Duration = 224 },
				new Track { TrackId = 16, AlbumId = 2, Position = 5, Name = "Highway Patrolman", Duration = 340 },
				new Track { TrackId = 17, AlbumId = 2, Position = 6, Name = "State Trooper", Duration = 197 },
				new Track { TrackId = 18, AlbumId = 2, Position = 7, Name = "Used Cars", Duration = 191 },
				new Track { TrackId = 19, AlbumId = 2, Position = 8, Name = "Open All Night", Duration = 178 },
				new Track { TrackId = 20, AlbumId = 2, Position = 9, Name = "My Father's Hous", Duration = 307 },
				new Track { TrackId = 21, AlbumId = 2, Position = 10, Name = "Reason to Believe", Duration = 251 },

				new Track { TrackId = 22, AlbumId = 3, Position = 1, Name = "On a Tuesday", Duration = 622 },
				new Track { TrackId = 23, AlbumId = 3, Position = 2, Name = "Tongue of God", Duration = 293 },
				new Track { TrackId = 24, AlbumId = 3, Position = 3, Name = "Meaningless", Duration = 287 },
				new Track { TrackId = 25, AlbumId = 3, Position = 4, Name = "Silent Gold", Duration = 203 },
				new Track { TrackId = 26, AlbumId = 3, Position = 5, Name = "Full Throttle Tribe", Duration = 545 },
				new Track { TrackId = 27, AlbumId = 3, Position = 6, Name = "Reasons", Duration = 285 },
				new Track { TrackId = 28, AlbumId = 3, Position = 7, Name = "Angels of Broken Things", Duration = 384 },
				new Track { TrackId = 29, AlbumId = 3, Position = 8, Name = "The Taming of a Beast", Duration = 393 },
				new Track { TrackId = 30, AlbumId = 3, Position = 9, Name = "If This Is the End", Duration = 303 },
				new Track { TrackId = 31, AlbumId = 3, Position = 10, Name = "The Passing Light of Day", Duration = 931 },

				new Track { TrackId = 32, AlbumId = 4, Position = 1, Name = "Thunder Road", Duration = 289 },
				new Track { TrackId = 33, AlbumId = 4, Position = 2, Name = "Tenth Avenue Freeze-Out", Duration = 191 },
				new Track { TrackId = 34, AlbumId = 4, Position = 3, Name = "Night", Duration = 180 },
				new Track { TrackId = 35, AlbumId = 4, Position = 4, Name = "Backstreets", Duration = 330 },
				new Track { TrackId = 36, AlbumId = 4, Position = 5, Name = "Born to Run", Duration = 281 },
				new Track { TrackId = 37, AlbumId = 4, Position = 6, Name = "She's the One", Duration = 280 },
				new Track { TrackId = 38, AlbumId = 4, Position = 7, Name = "Meeting Across the River", Duration = 191 },
				new Track { TrackId = 39, AlbumId = 4, Position = 8, Name = "Jungleland", Duration = 554 }
				);
		}
	}
}
