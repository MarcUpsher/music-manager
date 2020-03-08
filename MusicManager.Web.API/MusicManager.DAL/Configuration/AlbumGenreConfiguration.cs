using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MusicManager.DAL.Configuration
{
	class AlbumGenreConfiguration : IEntityTypeConfiguration<AlbumGenre>
	{
		public void Configure(EntityTypeBuilder<AlbumGenre> modelBuilder)
		{
			modelBuilder.ToTable("AlbumGenre");

			modelBuilder
				.HasKey(t => new { t.AlbumId, t.GenreId });

			modelBuilder
				.HasOne(pt => pt.Album)
				.WithMany(p => p.AlbumGenres)
				.HasForeignKey(pt => pt.AlbumId);

			modelBuilder
				.HasOne(pt => pt.Genre)
				.WithMany(t => t.AlbumGenres)
				.HasForeignKey(pt => pt.GenreId);

			modelBuilder
				.Property(s => s.DateAdded)
				.HasDefaultValue(DateTime.Now);
		}
	}
}
