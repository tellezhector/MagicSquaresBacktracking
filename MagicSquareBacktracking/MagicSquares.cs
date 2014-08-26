using System;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquareBacktracking
{
	public class MagicSquares
	{
		List<List<int>> _listOfMagicGroups;

		private List<List<int>> ListOfMagicGroups 
		{ 
			get
			{ 
				if (_listOfMagicGroups == null)
				{
					_listOfMagicGroups = new MagicGroups (Size).ListOfMagicGroups;
				} 

				return _listOfMagicGroups;
			} 
		}

		public int Counter { get; private set;}

		private int Size { get; set; }

		private int MagicNumber { get;	set; }

		public MagicSquares(int size)
		{
			Size = size;
			MagicNumber = (size * (size ^ 2 + 1)) / 2;
		}

		public void PrintMagicSquares()
		{
			Backtrack(new List<List<int>> (), 0);
		}

		private void TryPermutations (List<List<int>> current)
		{
			List<List<int>[]> listsPerm = PermutationsGenerator.Permutations (current);

			foreach (List<int>[] list in listsPerm)
			{
				TrySubPermutations(list, new int[Size, Size], 0);
			}
		}

		public static void PrintMatrix(int[,] m){
			m.PrintMatrix ();
		}

		private void TrySubPermutations (List<int>[] list, int[,] matrix, int counter)
		{
			var perms = PermutationsGenerator.Permutations (list [counter]);
			foreach(int[] perm in perms)
			{

				int[,] clone = (int[,])matrix.Clone ();
				for(int i=0; i< Size; i++)
				{
					clone[counter, i] = perm[i];
				}

				if (counter == Size - 1)
				{
					if (ValidMagicSquare (clone))
					{
						clone.PrintMatrix();
						Counter++;
						Console.WriteLine("{0} squares found so far.", Counter);
						Console.WriteLine ();
					}

					continue;
				}

				if (IsValidOption(clone))
				{
					TrySubPermutations (list, clone, counter + 1);
				}
			}
		}

		static bool IsValidOption (int[,] option)
		{
			var size = option.GetLength(0);

			int diagonalSum = 0;
			int invertedDiagonalSum = 0;

			int diagonalCounter = 0;
			int invertedDiagonalCounter = 0;
			for (int i = 0; i < size; i++) {
				if (option [i, i] > 0) 
				{
					diagonalCounter++;
				}

				diagonalSum += option [i, i];

				if (option [size - 1 - i, i] > 0) 
				{
					invertedDiagonalCounter++;
				}

				invertedDiagonalSum += option [size-1-i, i];


				int horizontalCounter = 0;
				int verticalCounter = 0;

				int currentHorizontalSum = 0;
				int currentVerticalSum = 0;

				for (int j = 0; j < size; j++) 
				{
					if (option [i, j] > 0) 
					{
						horizontalCounter++;
					}
					currentHorizontalSum += option[i, j];

					if (option [j, i] > 0) 
					{
						verticalCounter++;
					}
					currentVerticalSum += option[j, i];
				}

				if (!(PartialSumValidation (size, horizontalCounter, currentHorizontalSum) && PartialSumValidation (size, verticalCounter, currentVerticalSum))) 
				{
					return false;
				}
			}

			if (!(PartialSumValidation (size, diagonalCounter, diagonalSum) && PartialSumValidation (size, invertedDiagonalCounter, invertedDiagonalSum))) {
				return false;
			}

			return true;
		}

		public static bool PartialSumValidation(int size,  int counter, int partialSum){

			int magicNumber = size * (size * size + 1) / 2;
			if (partialSum > magicNumber) 
			{
				return false;
			}

			if (size == counter && partialSum != magicNumber) 
			{
				return false;
			}

			int square = size * size;
			int totalSum = (square * (square + 1)) / 2;
			int bestPossibleSum = ((square + counter - size) * ((square + counter - size) + 1)) / 2;

			if (partialSum < magicNumber && (magicNumber - partialSum > totalSum - bestPossibleSum)) 
			{
				return false;
			}

			return true;
		}

		private bool ValidMagicSquare(int[,] square)
		{
			var magicNumber = (Size * (Size * Size + 1)) / 2;
			int diagonalSum = 0;
			int invertedDiagonalSum = 0;
			for (int i = 0; i < Size; i++) {
				diagonalSum += square [i, i];
				invertedDiagonalSum += square [Size-1-i, i];
				int currentHorizontalSum = 0;
				int currentVerticalSum = 0;

				for (int j = 0; j < Size; j++) {
					currentHorizontalSum += square[i, j];
					currentVerticalSum += square[j, i];
				}

				if (currentVerticalSum != magicNumber || currentHorizontalSum != magicNumber) {
					return false;
				}
			}

			if (diagonalSum != magicNumber || invertedDiagonalSum != magicNumber) {
				return false;
			}

			return true;
		}

		public static void PrintCurrent(List<List<int>> current){
			foreach (List<int> c in current)
			{
				Console.WriteLine (string.Join(",",c));
			}

			Console.WriteLine();
		}

		public void Backtrack(List<List<int>> current, int index)
		{
			if (ListOfMagicGroups.Count - index < Size - current.Count)
			{
				return;
			}

			if (current.Count == Size) 
			{
				TryPermutations (current);
			}
				
			for (int i = index; i < ListOfMagicGroups.Count; i++)
			{
				if(Accepts(current, ListOfMagicGroups[i]))
				{
					Backtrack (current.Concat (new List<List<int>>{ ListOfMagicGroups[i] }).ToList(), i + 1);
				}
			}
		}

		bool Accepts (List<List<int>> current, List<int> list)
		{
			return !current.Any (l => l.Any (i => list.Any (j => j == i)));
		}
	}
	
}