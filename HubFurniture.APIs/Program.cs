using HubFurniture.APIs.Errors;
using HubFurniture.APIs.Extensions;
using HubFurniture.APIs.Helpers;
using HubFurniture.APIs.Middlewares;
using HubFurniture.Repository.Data;
using HubFurniture.Repository.DataSeed;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HubFurniture.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "JWTToken_Auth_API",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme.\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
                });
            });


            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            builder.Services.AddApplicationServices(builder.Configuration);

            #endregion

            var app = builder.Build();

            var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<StoreContext>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await dbContext.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(dbContext);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error is happened during update database");
            }


            #region Configure Kestrel Middlewares

            // Configure the HTTP request pipeline.

            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseStaticFiles();

            app.UseCors("Default");


            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.MapControllers();

            #endregion

            app.Run();
        }
    }
}
