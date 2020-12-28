using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Rpg_Restapi.Data;

namespace Rpg_Restapi {
  public class Program {
    public static void Main (string[] args) {

      var host = CreateHostBuilder (args).Build ();

      using (var scope = host.Services.CreateScope ()) {
        var db = scope.ServiceProvider.GetRequiredService<DataContext> ();
        db.Database.Migrate ();
      }
      host.Run ();

      // CreateHostBuilder (args)
      //   .Build ()
      //   .Run ();
    }

    public static IHostBuilder CreateHostBuilder (string[] args) {
      return Host.CreateDefaultBuilder (args)
        .ConfigureWebHostDefaults ((webBuilder) => {
          webBuilder.UseUrls ($"http://+:{HostPort}");
          webBuilder.UseKestrel ();
          webBuilder.UseStartup<Startup> ();
        });
      // return Host.CreateDefaultBuilder (args)
      //   .ConfigureWebHostDefaults (webBuilder => {
      //     webBuilder.UseStartup<Startup> ();
      //   });

    }

    private static bool IsDevelopment =>
      Environment.GetEnvironmentVariable ("ASPNETCORE_ENVIRONMENT") == "Development";

    public static string HostPort =>
      IsDevelopment ?
      "5000" :
      Environment.GetEnvironmentVariable ("PORT");

  }
}