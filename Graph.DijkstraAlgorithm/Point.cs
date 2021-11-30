using System;
using System.Collections.Generic;

namespace Graph.DijkstraAlgorithm
{
	class Point : IEquatable<Point>, IComparable<Point>
	{
		public string Name { get; set; }

		public double PathValue { get; set; }

		public bool IsChecked { get; set; }

		public List<Path> Paths { get; set; }

		public Point(string name, double pathValue = double.MaxValue)
		{
			Name = name;
			PathValue = pathValue;
			Paths = new List<Path>();
		}

		public bool Equals(Point other)
		{
			return Name.Equals(other.Name);
		}

		public int CompareTo(Point other)
		{
			if (PathValue > other.PathValue) return 1;
			if (PathValue < other.PathValue) return -1;
			return 0;
		}
	}
}