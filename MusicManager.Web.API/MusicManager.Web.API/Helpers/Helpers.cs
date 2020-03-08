using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MusicManager.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicManager.Web.API.Helpers
{
	public static class Helpers
	{
    public static IHost MigrateDatabase<T>(this IHost webHost) where T : DbContext
    {
      using (var scope = webHost.Services.CreateScope())
      {
        var services = scope.ServiceProvider;
        try
        {
          var db = services.GetRequiredService<T>();
          db.Database.Migrate();
        }
        catch (Exception ex)
        {
          var logger = services.GetRequiredService<ILogger<Program>>();
          logger.LogError(ex, "An error occurred while migrating the database.");
        }
      }
      return webHost;
    }
  }

  public static class MigrationManager
  {
    public static IHost MigrateDatabase(this IHost host)
    {
      using (var scope = host.Services.CreateScope())
      {
        using (var appContext = scope.ServiceProvider.GetRequiredService<MusicManagerContext>())
        {
          try
          {
            appContext.Database.Migrate();
          }
          catch (Exception ex)
          {
            //Log errors or do anything you think it's needed
            throw;
          }
        }
      }

      return host;
    }
  }
}
