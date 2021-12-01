using System;

namespace Sort.QuickSort
{
	class Program
	{
		static void Main(string[] args)
		{
			int[] array = new int[] { 999, -233, -22, 55, 9, 5, 8, 2, 14, 234, -1, 0, 16, -9, 4, 5, -24, 1, 555 };

			Sorter.QuickSort(array);
			PrintArray(array);

			Console.ReadKey();
		}

		private static void PrintArray(int[] array)
		{
			Console.WriteLine(string.Join("  ", array));
		}
	}
}