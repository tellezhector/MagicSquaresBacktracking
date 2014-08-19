using System;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquareBacktracking
{

	class MainClass
	{
		public static int globalCounter = 0;

		public static void Main (string[] args)
		{
			MagicGroups m = new MagicGroups (4);
			List<int> list = m.ListOfMagicGroups.First ();

			List<int[]> permutations = PermutationsGenerator.Permutations(list.ToArray());

			foreach (var p in permutations) {
				Console.WriteLine (string.Join (",", p));
			}

			Console.WriteLine("{0} permutations", permutations.Count);
		}

		public static void Backtracking(int step, int[,] option)
		{
			bool isLastStep = step == option.GetLength(0) * option.GetLength(1);

			if (isLastStep) {
				if (ValidMagicSquare (option)) {
					PrintMatrix (option);
					globalCounter++;
					Console.WriteLine ("{0} results found so far.", globalCounter);
					Console.WriteLine ();
					return;
				}
			}

			List<int[,]> suboptions = GenerateOptions (step, option);
			foreach (var suboption in suboptions) {
				if (IsValidOption (step + 1, suboption)) {
					Backtracking (step + 1, suboption);
				}
			}
		}

		static bool IsValidOption (int step, int[,] option)
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

				if (!(PartialSumValidation (size, step, horizontalCounter, currentHorizontalSum) && PartialSumValidation (size, step, verticalCounter, currentVerticalSum))) 
				{
					return false;
				}
			}

			if (!(PartialSumValidation (size, step, diagonalCounter, diagonalSum) && PartialSumValidation (size, step, invertedDiagonalCounter, invertedDiagonalSum))) {
				return false;
			}

			return true;
		}

		public static bool PartialSumValidation(int size, int step,  int counter, int partialSum){
		
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

			if (partialSum < magicNumber && (magicNumber - partialSum <= step || magicNumber - partialSum > totalSum - bestPossibleSum)) 
			{
				return false;
			}

			return true;
		}

		public static bool ValidMagicSquare(int[,] square)
		{
			var n = square.GetLength(0);

			var magicNumber = (n * (n * n + 1)) / 2;
			int diagonalSum = 0;
			int invertedDiagonalSum = 0;
			for (int i = 0; i < n; i++) {
				diagonalSum += square [i, i];
				invertedDiagonalSum += square [n-1-i, i];
				int currentHorizontalSum = 0;
				int currentVerticalSum = 0;

				for (int j = 0; j < n; j++) {
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

		static List<int[,]> GenerateOptions (int step, int[,] option)
		{
			var n = option.GetLength(0);
			var m = option.GetLength(1);

			List<int[,]> options = new List<int[,]> ();

			for (int i = 0; i < n; i++) {
				for (int j = 0; j < m; j++) {
					if (option [i, j] == 0) {
						var o = (int[,])option.Clone();
						o [i, j] = step + 1;
						options.Add (o);
					}
				}
			}

			return options;
		}

		public static void PrintMatrix(int[,] matrix)
		{
			var n = matrix.GetLength(0);
			var m = matrix.GetLength(1);

			StringBuilder builder = new StringBuilder ();

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < m; j++) 
				{
					builder.Append (matrix [i, j].ToString ().PadRight (3));
				}
				builder.AppendLine();
			} 


			builder.AppendLine();
			Console.WriteLine(builder.ToString());
		}
	}
}