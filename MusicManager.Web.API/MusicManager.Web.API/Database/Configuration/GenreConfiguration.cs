using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicManager.Web.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicManager.DAL.Configuration
{
	class GenreConfiguration : IEntityTypeConfiguration<Genre>
	{
		public void Configure(EntityTypeBuilder<Genre> modelBuilder)
		{
			modelBuilder.ToTable("Genre");

			modelBuilder
				.HasKey(s => s.GenreId);

			modelBuilder
				.Property(s => s.Name)
				.HasMaxLength(300);

			modelBuilder
				.Property(s => s.DateAdded)
				.HasDefaultValue(DateTime.Now);
		}
	}
}
