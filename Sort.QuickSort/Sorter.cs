using System;

namespace Sort.QuickSort
{
	public static class Sorter
	{
		public static void QuickSort<T>(T[] array) where T : IComparable<T>
		{
			QuickSortInternal(array, 0, array.Length - 1);
		}

		private static void QuickSortInternal<T>(T[] array, int leftIndex, int rightIndex) where T : IComparable<T>
		{
			int pivotIndex = rightIndex;
			T pivotElement = array[pivotIndex];

			for (int i = leftIndex; i < pivotIndex; i++)
			{
				if (array[i].CompareTo(pivotElement) > 0)
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

		private static void InsertElementAfterPivot<T>(T[] array, int elementIndex, int pivotIndex)
		{
			T movedElement = array[elementIndex];

			for (int i = elementIndex; i < pivotIndex; i++)
			{
				array[i] = array[i + 1];
			}

			array[pivotIndex] = movedElement;
		}
	}
}