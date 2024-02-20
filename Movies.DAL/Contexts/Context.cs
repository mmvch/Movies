using Microsoft.EntityFrameworkCore;
using Movies.Domain.Models;

namespace Movies.DAL.Contexts
{
	public class Context(DbContextOptions<Context> options) : DbContext(options)
	{
		public DbSet<Film> Films { get; set; }
	}
}
