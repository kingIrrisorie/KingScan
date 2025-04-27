using APIManga.Context;
using APIManga.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace APIManga
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddDbContext<KingIrrisorieScanContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

			builder.Services.AddScoped<MangaService>();

			builder.Services.AddControllers();

			// Configuração de CORS
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowFrontend", policy =>
				{
					policy.WithOrigins("http://127.0.0.1:8080", "https://192.168.0.112:8080")
						  .AllowAnyMethod()
						  .AllowAnyHeader()
						  .AllowCredentials();
				});
			});

			// Configure Swagger/OpenAPI
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "APIManga",
					Version = "v1",
					Description = "API para tradução e gerenciamento de mangás e manhwas",
					Contact = new OpenApiContact
					{
						Name = "King Irrisorie",
						Email = "contato@kingirrisorie.com"
					}
				});
			});

			// Configure Kestrel para ouvir em todos os IPs
			builder.WebHost.ConfigureKestrel(options =>
			{
				options.ListenAnyIP(5215, listenOptions =>
				{
					listenOptions.UseHttps();
				});
			});

			var app = builder.Build();

			app.UseCors("AllowFrontend");

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIManga v1");
			});

			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}
}