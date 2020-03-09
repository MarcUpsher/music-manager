using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicManager.DAL.Configuration
{
	class AlbumConfiguration : IEntityTypeConfiguration<Album>
	{
		public void Configure(EntityTypeBuilder<Album> modelBuilder)
		{
			modelBuilder.ToTable("Album");

			modelBuilder
				.HasKey(s => s.AlbumId);

			modelBuilder.HasOne(s => s.Artist)
				.WithMany(g => g.Albums)
				.HasForeignKey(s => s.ArtistId);

			modelBuilder.HasOne(s => s.ImageRef)
				.WithMany(g => g.Albums)
				.HasForeignKey(s => s.ImageRefId);

			modelBuilder.Property(s => s.DateAdded)
				.IsRequired()
				.HasDefaultValue(DateTime.Now);

			modelBuilder.Property(s => s.Name)
				.IsRequired()
				.HasMaxLength(300);

			modelBuilder.Property(s => s.Summary)
				.HasMaxLength(2000);
		}
	}
}
