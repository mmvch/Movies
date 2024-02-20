using Microsoft.Extensions.DependencyInjection;
using Movies.DAL.Repositories;
using Movies.Domain.Exceptions;
using Movies.Domain.Models;
using System.Net;

namespace Movies.DAL.Services
{
	public class FilmService(IServiceProvider services) : IFilmService
	{
		private readonly IRepository<Film, Guid> _filmRepository = services.GetRequiredService<IRepository<Film, Guid>>();

		public async Task<IEnumerable<Film>> GetAllAsync()
		{
			return await _filmRepository.GetAllAsync();
		}

		public async Task<Film> GetByIdAsync(Guid id)
		{
			return await _filmRepository.GetByIdAsync(id) ??
				throw new ServiceException("Film does not exist", HttpStatusCode.NotFound);
		}

		public async Task CreateAsync(Film film)
		{
			try
			{
				await _filmRepository.CreateAsync(film);
				await _filmRepository.SaveAsync();
			}
			catch (Exception)
			{
				throw new ServiceException("Film creating error", HttpStatusCode.BadRequest);
			}
		}

		public async Task UpdateAsync(Film film)
		{
			try
			{
				_filmRepository.Update(film);
				await _filmRepository.SaveAsync();
			}
			catch (Exception)
			{
				throw new ServiceException("Film updating error", HttpStatusCode.BadRequest);
			}
		}

		public async Task DeleteAsync(Guid id)
		{
			Film film = await _filmRepository.GetByIdAsync(id) ??
				throw new ServiceException("Film does not exist", HttpStatusCode.NotFound);

			try
			{
				_filmRepository.Delete(film);
				await _filmRepository.SaveAsync();
			}
			catch (Exception)
			{
				throw new ServiceException("Film deleting error", HttpStatusCode.BadRequest);
			}
		}
	}
}
