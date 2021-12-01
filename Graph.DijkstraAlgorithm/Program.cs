using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph.DijkstraAlgorithm
{
	class Program
	{
		static void Main(string[] args)
		{
			Point p1 = new Point("1");
			Point p2 = new Point("2");
			Point p3 = new Point("3");
			Point p4 = new Point("4");
			Point p5 = new Point("5");

			Path path12 = new Path(p1, p2, 7);
			Path path13 = new Path(p1, p3, 9);
			Path path16 = new Path(p1, p5, 14);

			Path path23 = new Path(p2, p3, 10);
			Path path24 = new Path(p2, p4, 15);

			Path path34 = new Path(p3, p4, 11);
			Path path36 = new Path(p3, p5, 2);

			//Path path45 = new Path(p4, p5, 6);
			//Path path65 = new Path(p6, p5, 9);

			var t = FindShortestPathForWeightedGraph(p1, p5);
			foreach (var poin in t) 
			{
				Console.WriteLine(poin.Name);
			}

			Console.ReadKey();
		}

		static List<Point> FindShortestPathForWeightedGraph(Point startPoint, Point endPoint)
		{
			startPoint.PathValue = 0;
			List<Point> uncheckedPointsPool = new List<Point>() { startPoint };

			while (uncheckedPointsPool.Count > 0)
			{
				var pointWithMinPathValue = GetPointWithMinPathsValue(uncheckedPointsPool);
				//Console.WriteLine(pointWithMinPathValue.Name);

				foreach (var path in pointWithMinPathValue.Paths)
				{
					Point neighbour = path.GetOppositePointFrom(pointWithMinPathValue);

					if (neighbour.IsChecked || (path.IsOneWayFromAToB && neighbour == path.PointA)) continue;

					if (path.Value + pointWithMinPathValue.PathValue < neighbour.PathValue)
					{
						neighbour.PathValue = path.Value + pointWithMinPathValue.PathValue;
						//Console.WriteLine(neighbour.Name + " " + neighbour.PathValue);
					}

					if (!uncheckedPointsPool.Contains(neighbour)) uncheckedPointsPool.Add(neighbour);
				}

				pointWithMinPathValue.IsChecked = true;
			}

			// if path not found
			if (endPoint.PathValue == double.MaxValue) return null;

			// define path
			List<Point> interjacentPoints = new List<Point>() { endPoint };

			Point currentPoint = endPoint;

			while (currentPoint != startPoint)
			{
				currentPoint = currentPoint.Paths
					.Where(p => currentPoint.PathValue - p.Value == p.GetOppositePointFrom(currentPoint).PathValue)
					.Select(p => p.GetOppositePointFrom(currentPoint))
					.First();

				interjacentPoints.Add(currentPoint);
			}

			return interjacentPoints.OrderBy(p => p.PathValue).ToList();
		}

		static Point GetPointWithMinPathsValue(List<Point> pointsSet)
		{
			var pointWithMinPathsValue = pointsSet.Min<Point>();
			pointsSet.Remove(pointWithMinPathsValue);

			return pointWithMinPathsValue;
		}
	}
}