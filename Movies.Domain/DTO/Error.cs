namespace Movies.Domain.DTO
{
	public class Error
	{
		public int Status { get; set; } = 500;
		public string Message { get; set; } = "Internal Server Error";
	}
}
