using System.ComponentModel.DataAnnotations;

namespace Movies.Domain.DTO
{
	public class UpdateFilmDTO
	{
		[Required(ErrorMessage = "Id is required")]
		public Guid Id { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }
	}
}
