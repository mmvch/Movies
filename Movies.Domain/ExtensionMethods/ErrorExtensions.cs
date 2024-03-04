using Movies.Domain.DTO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Movies.Domain.ExtensionMethods
{
	public static class ErrorExtensions
	{
		public static string ToJson(this Error error)
		{
			return JsonSerializer.Serialize(error);
		}

		public static string ToXml(this Error error)
		{
			XmlSerializer serializer = new(typeof(Error));
			using StringWriter writer = new();
			serializer.Serialize(writer, error);
			return writer.ToString();
		}
	}
}
