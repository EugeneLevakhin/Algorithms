using System.Collections.Generic;
using CurrencyFeed.Entities;

namespace CurrencyFeed.Events
{
	public delegate void CurrenciesLoadedEventHandler(List<Organization> organizations);
}