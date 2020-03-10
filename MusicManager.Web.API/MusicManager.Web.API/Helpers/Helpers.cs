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

namespace MusicManager.Web.API
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

    public static string GetImageUri(string uri)
    {
      return "http://localhost:5000/" + uri;
    }
  }  
}
