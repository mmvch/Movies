using Microsoft.EntityFrameworkCore;
using Movies.DAL.Contexts;
using Movies.DAL.Repositories;
using Movies.DAL.Services;
using Movies.Domain.Models;
using Movies.Middleware;

namespace Movies
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddScoped<IFilmService, FilmService>();
			builder.Services.AddScoped<IRepository<Film, Guid>, Repository<Film, Guid>>();

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
				throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

			builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var serviceProvider = scope.ServiceProvider.GetRequiredService<Context>();
				serviceProvider.Database.Migrate();
			}

			app.UseMiddleware<ExceptionHandler>();
			app.UseHttpsRedirection();
			app.MapControllers();
			app.UseRouting();
			app.Run();
		}
	}
}
