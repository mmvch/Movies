using Microsoft.AspNetCore.Mvc;
using Movies.DAL.Services;
using Movies.Domain.DTO;
using Movies.Domain.Models;

namespace Movies.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FilmController(IFilmService filmService) : ControllerBase
	{
		private readonly IFilmService _filmService = filmService;

		[HttpGet]
		public async Task<IEnumerable<Film>> GetAllAsync()
		{
			return await _filmService.GetAllAsync();
		}

		[HttpGet("{id}")]
		public async Task<Film> GetByIdAsync([FromRoute] Guid id)
		{
			return await _filmService.GetByIdAsync(id);
		}

		[HttpPost]
		public async Task CreateAsync([FromBody] CreateFilmDTO filmDTO)
		{
			Film film = new()
			{
				Id = Guid.NewGuid(),
				Name = filmDTO.Name,
				Description = filmDTO.Description
			};

			await _filmService.CreateAsync(film);
		}

		[HttpPut]
		public async Task UpdateAsync([FromBody] UpdateFilmDTO filmDTO)
		{
			Film film = await _filmService.GetByIdAsync(filmDTO.Id);

			if (!string.IsNullOrWhiteSpace(filmDTO.Name))
			{
				film.Name = filmDTO.Name;
			}

			if (!string.IsNullOrWhiteSpace(filmDTO.Description))
			{
				film.Description = filmDTO.Description;
			}

			await _filmService.UpdateAsync(film);
		}

		[HttpDelete("{id}")]
		public async Task Delete([FromRoute] Guid id)
		{
			await _filmService.DeleteAsync(id);
		}
	}
}
