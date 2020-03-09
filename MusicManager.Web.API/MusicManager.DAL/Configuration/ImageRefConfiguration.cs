using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicManager.DAL.Configuration
{
	class ImageRefConfiguration : IEntityTypeConfiguration<ImageRef>
	{
		public void Configure(EntityTypeBuilder<ImageRef> modelBuilder)
		{
			modelBuilder.ToTable("ImageRef");

			modelBuilder
				.HasKey(s => s.ImageRefId);

			modelBuilder.Property(s => s.DateAdded)
				.IsRequired()
				.HasDefaultValue(DateTime.Now);
		}
	}
}
