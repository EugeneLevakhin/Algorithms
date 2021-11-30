namespace CurrencyFeed.Entities
{
	public class Currency
	{
		public string Id { get; set; }

		public double SellingCourse { get; set; }

		public double BuingCourse { get; set; }

		public Currency(string id, double buingCourse, double sellingCourse)
		{
			Id = id;
			SellingCourse = sellingCourse;
			BuingCourse = buingCourse;
		}
	}
}