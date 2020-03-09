using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicManager.DAL.Configuration
{
	class TrackConfiguration : IEntityTypeConfiguration<Track>
	{
		public void Configure(EntityTypeBuilder<Track> modelBuilder)
		{
			modelBuilder.ToTable("Track");

			modelBuilder
				.HasKey(s => s.TrackId);

			modelBuilder.HasOne(s => s.Album)
				.WithMany(g => g.Tracks)
				.HasForeignKey(s => s.AlbumId);

			modelBuilder
				.Property(s => s.Name)
				.HasMaxLength(300);

			modelBuilder
				.Property(s => s.DateAdded)
				.HasDefaultValue(DateTime.Now);
		}
	}
}
