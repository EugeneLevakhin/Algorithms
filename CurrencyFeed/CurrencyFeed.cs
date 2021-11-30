using System;
using System.Net;
using System.Text;
using System.Xml;
using System.Collections.Generic;
using CurrencyFeed.Entities;
using CurrencyFeed.Events;

namespace CurrencyFeed
{
	public class CurrencyFeed
	{
		private string XMLText;
		private Uri resourceURIAddress;
		private WebClient webClient;
		private InfoDictionaries infoDictionaries;
		private List<Organization> orgaizations;

		public event CurrenciesLoadedEventHandler CurrenciesLoaded;

		public CurrencyFeed()
		{
			XMLText = string.Empty;
			resourceURIAddress = new Uri("http://resources.finance.ua/ru/public/currency-cash.xml");
			webClient = new WebClient();
			webClient.DownloadStringCompleted += WebClient_DownloadStringCompleted;
		}

		public void LoadCurrencies()
		{
			webClient.DownloadStringAsync(resourceURIAddress);
		}

		private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			XMLText = e.Result;
			XMLText = Encoding.UTF8.GetString(Encoding.Default.GetBytes(XMLText));  // convert in right view string, because info in russian text
			LoadRawData();
			TransformRawData();
		}

		private void LoadRawData()
		{
			infoDictionaries = new InfoDictionaries();
			orgaizations = new List<Organization>();

			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(XMLText);

			XmlElement xmlRoot = xmlDocument.DocumentElement;
			string dateString = xmlRoot.Attributes.GetNamedItem("date").Value;

			DateTime date = DateTime.Parse(dateString);
			Console.WriteLine(date);

			foreach (XmlNode childnode in xmlRoot)
			{
				if (childnode.Name == "organizations")
				{
					foreach (XmlNode orgItem in childnode.ChildNodes)
					{
						Organization organization = new Organization();
						organization.Id = orgItem.Attributes.GetNamedItem("id").Value;
						organization.OldId = orgItem.Attributes.GetNamedItem("oldid").Value;
						organization.OrgType = orgItem.Attributes.GetNamedItem("org_type").Value;

						foreach (XmlNode organizationMember in orgItem)
						{
							if (organizationMember.Name == "title")
							{
								organization.Title = organizationMember.Attributes.GetNamedItem("value").Value;
							}
							else if (organizationMember.Name == "branch")
							{
								organization.IsBranch = Convert.ToBoolean(organizationMember.Attributes.GetNamedItem("value").Value);
							}
							else if (organizationMember.Name == "region")
							{
								organization.Region = organizationMember.Attributes.GetNamedItem("id").Value;
							}
							else if (organizationMember.Name == "city")
							{
								organization.City = organizationMember.Attributes.GetNamedItem("id").Value;
							}
							else if (organizationMember.Name == "phone")
							{
								organization.Phone = organizationMember.Attributes.GetNamedItem("value").Value;
							}
							else if (organizationMember.Name == "address")
							{
								organization.Adress = organizationMember.Attributes.GetNamedItem("value").Value;
							}
							else if (organizationMember.Name == "link")
							{
								organization.Link = new Link(organizationMember.Attributes.GetNamedItem("type").Value, organizationMember.Attributes.GetNamedItem("href").Value);
							}
							else if (organizationMember.Name == "currencies")
							{
								List<Currency> currencies = new List<Currency>();

								foreach (XmlNode currencyItem in organizationMember)
								{
									currencies.Add(new Currency(currencyItem.Attributes.GetNamedItem("id").Value, Convert.ToDouble(currencyItem.Attributes.GetNamedItem("br").Value), Convert.ToDouble(currencyItem.Attributes.GetNamedItem("ar").Value)));
								}
								organization.Currencies = currencies;
							}
						}
						orgaizations.Add(organization);
					}
				}

				else if (childnode.Name == "org_types")
				{
					foreach (XmlNode orgType in childnode.ChildNodes)
					{
						infoDictionaries.OrgTypes.Add(orgType.Attributes.GetNamedItem("id").Value, orgType.Attributes.GetNamedItem("title").Value);
					}
				}

				else if (childnode.Name == "currencies")
				{
					foreach (XmlNode currency in childnode.ChildNodes)
					{
						infoDictionaries.Currencies.Add(currency.Attributes.GetNamedItem("id").Value, currency.Attributes.GetNamedItem("title").Value);
					}
				}

				else if (childnode.Name == "regions")
				{
					foreach (XmlNode region in childnode.ChildNodes)
					{
						infoDictionaries.Regions.Add(region.Attributes.GetNamedItem("id").Value, region.Attributes.GetNamedItem("title").Value);
					}
				}

				else if (childnode.Name == "cities")
				{
					foreach (XmlNode city in childnode.ChildNodes)
					{
						infoDictionaries.Cities.Add(city.Attributes.GetNamedItem("id").Value, city.Attributes.GetNamedItem("title").Value);
					}
				}
			}
		}

		private void TransformRawData()
		{
			foreach (var organization in orgaizations)
			{
				organization.OrgType = infoDictionaries.OrgTypes[organization.OrgType];
				organization.Region = infoDictionaries.Regions[organization.Region];
				organization.City = infoDictionaries.Cities[organization.City];
			}

			CurrenciesLoaded?.Invoke(orgaizations);
		}
	}
}
