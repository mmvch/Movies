using Movies.Domain.Models;

namespace Movies.DAL.Services
{
	public interface IFilmService
	{
		Task<IEnumerable<Film>> GetAllAsync();
		Task<Film> GetByIdAsync(Guid id);
		Task CreateAsync(Film film);
		Task UpdateAsync(Film film);
		Task DeleteAsync(Guid id);
	}
}
