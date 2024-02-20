namespace Movies.Domain.Models
{
	public class Film
	{
		public Guid Id { get; set; }

		public string? Name { get; set; }

		public string? Description { get; set; }
	}
}
