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

    public static IHostBuilder CreateHostBuilder (string[] args) =>
      Host.CreateDefaultBuilder (args)
      .ConfigureWebHostDefaults (webBuilder => {
        webBuilder.UseStartup<Startup> ();
      });

  }
}