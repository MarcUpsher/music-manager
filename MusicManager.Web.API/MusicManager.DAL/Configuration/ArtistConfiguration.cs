using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicManager.DAL.Configuration
{
	class ArtistConfiguration : IEntityTypeConfiguration<Artist>
	{
		public void Configure(EntityTypeBuilder<Artist> modelBuilder)
		{
			modelBuilder.ToTable("Artist");

			modelBuilder
				.HasKey(s => s.ArtistId);

			modelBuilder
				.Property(s => s.Name)
				.HasMaxLength(300);

			modelBuilder
				.Property(s => s.DateAdded)
				.HasDefaultValue(DateTime.Now);
		}
	}
}
