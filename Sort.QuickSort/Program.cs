using System;

namespace Sort.QuickSort
{
	class Program
	{
		static void Main(string[] args)
		{
			int[] array = new int[] { 999, -233, -22, 55, 9, 5, 8, 2, 14, 234, -1, 0, 16, -9, 4, 5, -24, 1, 555 };

			Sorter sorter = new Sorter();
			sorter.QuickSort(array);
			PrintArray(array);

			Console.ReadKey();
		}

		private static void PrintArray(int[] array)
		{
			Console.WriteLine(string.Join("  ", array));
		}
	}

	class Sorter
	{
		public void QuickSort(int[] array)
		{
			QuickSortInternal(array, 0, array.Length - 1);
		}

		private void QuickSortInternal(int[] array, int leftIndex, int rightIndex)
		{
			int pivotIndex = rightIndex;
			int pivotElement = array[pivotIndex];

			for (int i = leftIndex; i < pivotIndex; i++)
			{
				if (array[i] > pivotElement)
				{
					InsertElementAfterPivot(array, i, pivotIndex);
					pivotIndex--;
					i--;
				}
			}

			// Quick sort for left part
			if (pivotIndex - 1 > leftIndex)
			{
				QuickSortInternal(array, leftIndex, pivotIndex - 1);
			}

			// Quick sort for right part
			if (pivotIndex + 1 < rightIndex)
			{
				QuickSortInternal(array, pivotIndex + 1, rightIndex);
			}
		}

		private void InsertElementAfterPivot(int[] array, int elementIndex, int pivotIndex)
		{
			int movedElement = array[elementIndex];

			for (int i = elementIndex; i < pivotIndex; i++)
			{
				array[i] = array[i + 1];
			}

			array[pivotIndex] = movedElement;
		}
	}
}