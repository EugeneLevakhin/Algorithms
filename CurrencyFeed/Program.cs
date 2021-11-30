using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyFeed.Entities;

namespace CurrencyFeed
{
	class Program
	{
		static void Main(string[] args)
		{
			CurrencyFeed currency = new CurrencyFeed();
			currency.CurrenciesLoaded += Currency_CurrenciesLoaded;
			currency.LoadCurrencies();

			Console.ReadKey();
		}

		private static void Currency_CurrenciesLoaded(List<Organization> organizations)
		{
			var query = organizations.SelectMany(c => c.Currencies).Where(c => c.Id == "EUR" || c.Id == "RUB" || c.Id == "USD").GroupBy(gr => gr.Id);
			foreach (var item in query)
			{
				Console.Write(item.Key + ": ");
				Console.WriteLine(item.Max(a => a.BuingCourse));
			}
		}
	}
}