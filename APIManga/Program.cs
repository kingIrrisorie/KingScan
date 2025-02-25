using APIManga.Context;
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
				options.UseSqlServer(
					builder.Configuration.GetConnectionString("ConexaoPadrao")));

			builder.Services.AddControllers();

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

			// Configure the HTTP request pipeline.
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