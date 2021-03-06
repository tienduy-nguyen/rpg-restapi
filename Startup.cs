using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Rpg_Restapi.Data;
using Rpg_Restapi.Services;

namespace Rpg_Restapi {
  public class Startup {
    public Startup (IConfiguration configuration, IWebHostEnvironment env) {
      Configuration = configuration;
      _WebHostEnvironment = env;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment _WebHostEnvironment { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices (IServiceCollection services) {
      services.AddControllers ()
        .AddNewtonsoftJson (options => options.UseMemberCasing ());
      services.AddHttpContextAccessor ();

      /* Connect database) */
      string connectionString = _WebHostEnvironment.IsDevelopment () ?
        Configuration.GetConnectionString ("DefaultConnection") :
        GetHerokuConnectionString ();

      services.AddDbContext<DataContext> (options => {
        options.UseNpgsql (connectionString);
      });

      /*  Auto mapper class - Dto*/
      services.AddAutoMapper (AppDomain.CurrentDomain.GetAssemblies ());

      /* Denpendency Injection for service */
      services.AddScoped<ICharacterService, CharacterService> ();
      services.AddScoped<IAuthService, AuthService> ();
      services.AddScoped<IUserService, UserService> ();
      services.AddScoped<ISkillService, SkillService> ();
      services.AddScoped<IWeaponService, WeaponService> ();
      services.AddScoped<ICharacterSkillService, CharacterSkillService> ();
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor> ();
      services.AddScoped<IFightService, FightService> ();

      /* Swagger doc */
      services.AddSwaggerGen (c => {
        c.SwaggerDoc ("v1", new OpenApiInfo {
          Title = "Role Playing Game API",
            Version = "v1",
            Description = "Documentation for RPG API"
        });
        c.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme () {
          Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        });
        c.AddSecurityRequirement (new OpenApiSecurityRequirement {
          {
            new OpenApiSecurityScheme {
              Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                  Id = "Bearer"
              }
            },
            new string[] { }

          }
        });

        // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine (AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments (xmlPath);

      });

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

      app.UseAuthentication ();
      app.UseAuthorization ();

      app.UseEndpoints (endpoints => {
        endpoints.MapControllers ();
      });
    }

    /* Helpers methods for heroku postgres database */

    private string GetHerokuConnectionString () {
      // Get the connection string from the ENV variables
      string connectionUrl = System.Environment.GetEnvironmentVariable ("DATABASE_URL");

      // parse the connection string
      var databaseUri = new Uri (connectionUrl);

      string db = databaseUri.LocalPath.TrimStart ('/');
      string[] userInfo = databaseUri.UserInfo.Split (':', StringSplitOptions.RemoveEmptyEntries);

      return $"User ID={userInfo[0]};Password={userInfo[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={db};Pooling=true;SSL Mode=Require;Trust Server Certificate=True;";
    }

  }
}