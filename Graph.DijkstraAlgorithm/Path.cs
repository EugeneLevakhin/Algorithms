namespace Graph.DijkstraAlgorithm
{
	class Path
	{
		public double Value { get; set; }

		public Point PointA { get; set; }

		public Point PointB { get; set; }

		public bool IsOneWayFromAToB { get; set; }

		public Path(Point pointA, Point pointB, double value, bool oneWayFromAToB = false)
		{
			PointA = pointA;
			PointB = pointB;
			IsOneWayFromAToB = oneWayFromAToB;

			Value = value;

			PointA.Paths.Add(this);
			PointB.Paths.Add(this);
		}

		public Point GetOppositePointFrom(Point point)
		{
			if (point.Equals(PointA))
			{
				return PointB;
			}
			else if (point.Equals(PointB))
			{
				return PointA;
			}
			return null;
		}
	}
}