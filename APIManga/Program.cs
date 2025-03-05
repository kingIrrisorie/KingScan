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

			builder.Services.AddScoped<MangaService>(); // Registro do serviço

			builder.Services.AddControllers();

			// Configuracao de CORS
			//builder.Services.AddCors(options =>
			//{
			//	options.AddPolicy("AllowFrontend", policy =>
			//	{
			//		policy.WithOrigins("http://127.0.0.1:5500")
			//		.AllowAnyMethod()
			//		.AllowAnyHeader();
			//	});
			//});

			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
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

			var app = builder.Build();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIManga v1");
			});

			//app.UseCors("AllowFrontend");
			app.UseCors("AllowAll");
			app.UseHttpsRedirection();
			app.UseAuthorization();
			app.MapControllers();

			app.Run();
		}
	}
}