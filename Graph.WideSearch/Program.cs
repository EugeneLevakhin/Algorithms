using System;
using System.Collections.Generic;

namespace Graph.WideSearch
{
	class Program
	{
		static void Main(string[] args)
		{
			Person i = new Person("I");
			Person alice = new Person("Alice");
			Person kler = new Person("Kler");
			Person tom = new Person("Tom");
			Person jonny = new Person("Jonny");
			Person bob = new Person("Bob");
			Person peggi = new Person("Peggi");
			Person anuj = new Person("Anuj", null, true);

			i.Friends.Add(alice);
			i.Friends.Add(bob);
			i.Friends.Add(kler);

			kler.Friends.Add(i);
			kler.Friends.Add(tom);
			kler.Friends.Add(jonny);

			tom.Friends.Add(kler);

			jonny.Friends.Add(kler);

			alice.Friends.Add(i);
			alice.Friends.Add(peggi);

			peggi.Friends.Add(bob);
			peggi.Friends.Add(alice);

			bob.Friends.Add(i);
			bob.Friends.Add(peggi);
			bob.Friends.Add(anuj);

			anuj.Friends.Add(bob);

			var p = FindSeller(i);

			Console.ReadKey();
		}


		static Person FindSeller(Person person)
		{
			Queue<Person> personQueu = new Queue<Person>(person.Friends);
			List<Person> checkedPersons = new List<Person>();
			checkedPersons.Add(person);

			while (personQueu.Count > 0)
			{
				Person p = personQueu.Dequeue();

				if (p.IsSeller)
				{
					return p;
				}
				else
				{
					Console.WriteLine(p.Name);
					foreach (var item in p.Friends)
					{
						if (!checkedPersons.Contains(item) && !personQueu.Contains(item)) personQueu.Enqueue(item);
					}

					checkedPersons.Add(p);
				}
			}

			return null;
		}
	}

	class Person
	{
		public string Name { get; set; }

		public bool IsSeller { get; set; }

		public List<Person> Friends { get; set; }

		public Person(string name, List<Person> friends = null, bool isSaler = false)
		{
			if (friends == null) Friends = new List<Person>();
			Name = name;
			IsSeller = isSaler;
		}
	}
}