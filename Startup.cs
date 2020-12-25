using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Rpg_Restapi.Data;
using Rpg_Restapi.Services;

namespace Rpg_Restapi {
  public class Startup {
    public Startup (IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices (IServiceCollection services) {
      services.AddControllers ()
        .AddNewtonsoftJson (options => options.UseMemberCasing ());

      /* Config for Jwt */
      services.AddAuthentication (JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer (options => {
          options.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey (Encoding.ASCII
          .GetBytes (Configuration.GetSection ("AppSettings:Token").Value)),
          ValidateIssuer = false,
          ValidateAudience = false
          };
        });

      /* Handle json (for JsonPath) */
      services.AddDbContext<DataContext> (options => {
        options.UseNpgsql (Configuration.GetConnectionString ("DefaultConnection"));
      });

      /*  Auto mapper class - Dto*/
      services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());

      /* Denpendency Injection for service */
      services.AddScoped<ICharacterService, CharacterService> ();
      services.AddScoped<IAuthRepository, AuthRepository> ();

      /* Swagger doc */
      services.AddSwaggerGen (c => {
        c.SwaggerDoc ("v1", new OpenApiInfo {
          Title = "Role Playing Game API",
            Version = "v1",
            Description = "Documentation for RPG API"
        });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
      app.UseSwagger ();
      app.UseSwaggerUI (c => {
        c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Rpg api v1");
        c.RoutePrefix = string.Empty;
      });
      if (env.IsDevelopment ()) {
        app.UseDeveloperExceptionPage ();
      }

      app.UseHttpsRedirection ();

      app.UseRouting ();

      app.UseAuthorization ();
      app.UseAuthentication ();

      app.UseEndpoints (endpoints => {
        endpoints.MapControllers ();
      });
    }
  }
}