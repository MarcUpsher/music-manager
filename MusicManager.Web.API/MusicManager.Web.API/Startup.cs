using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MusicManager.Web.API.Database.Contexts;
using MusicManager.Web.API.Database.Repositories;
using MusicManager.Web.API.Domain.Repositories;
using MusicManager.Web.API.Domain.Services;
using MusicManager.Web.API.Services;

namespace MusicManager.Web.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		readonly string AllowedOrigins = "_allowedOrigins";

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContextPool<MusicManagerContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("sqlConnection"),
				providerOptions => providerOptions.EnableRetryOnFailure()));

			services.AddScoped<IGenreService, GenreService>();
			services.AddScoped<IGenreRepository, GenreRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddCors(options =>
			{
				options.AddPolicy(AllowedOrigins,
				builder =>
				{
					builder.WithOrigins("http://localhost:4200")
					.AllowAnyHeader()
					.AllowAnyMethod(); ;
				});
			});

			services.AddControllers();

			services.AddAutoMapper(typeof(Startup));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseFileServer();
			app.UseRouting();

			app.UseAuthorization();

			app.UseCors(AllowedOrigins);

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
