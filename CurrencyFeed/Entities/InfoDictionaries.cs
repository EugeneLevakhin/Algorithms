using System.Collections.Generic;

namespace CurrencyFeed.Entities
{
	class InfoDictionaries
	{
		public Dictionary<string, string> OrgTypes { get; set; }

		public Dictionary<string, string> Currencies { get; set; }

		public Dictionary<string, string> Regions { get; set; }

		public Dictionary<string, string> Cities { get; set; }

		public InfoDictionaries()
		{
			OrgTypes = new Dictionary<string, string>();
			Currencies = new Dictionary<string, string>();
			Regions = new Dictionary<string, string>();
			Cities = new Dictionary<string, string>();
		}
	}
}