using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.DTO
{
	public class CreateFilmDTO
	{
		[Required(ErrorMessage = "Name is required")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Description is required")]
		public string? Description { get; set; }
	}
}
