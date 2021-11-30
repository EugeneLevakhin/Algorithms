using System.Collections.Generic;

namespace CurrencyFeed.Entities
{
	public class Organization
	{
		public string Id { get; set; }

		public string OldId { get; set; }

		public string OrgType { get; set; }

		public string Title { get; set; }

		public bool IsBranch { get; set; }

		public string Region { get; set; }

		public string City { get; set; }

		public string Phone { get; set; }

		public string Adress { get; set; }

		public Link Link { get; set; }

		public List<Currency> Currencies { get; set; }
	}
}