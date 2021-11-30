namespace CurrencyFeed.Entities
{
	public class Link
	{
		public string Type { get; set; }

		public string HRef { get; set; }

		public Link(string type, string href)
		{
			Type = type;
			HRef = href;
		}
	}
}