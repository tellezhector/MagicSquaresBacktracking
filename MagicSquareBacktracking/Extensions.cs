using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicSquareBacktracking
{
	public static class Extensions
	{
		public static List<T> Clone<T>(this IEnumerable<T> listToClone)
		{
			return listToClone.Select(item => item).ToList();
		}

		public static void PrintMatrix(this int[,] matrix)
		{
			var n = matrix.GetLength(0);
			var m = matrix.GetLength(1);

			StringBuilder builder = new StringBuilder ();

			for (int i = 0; i < n; i++)
			{
				for (int j = 0; j < m; j++) 
				{
					builder.Append (matrix [i, j].ToString ().PadRight (4));
				}

				if(i < n-1)
				builder.AppendLine();
			} 

			Console.WriteLine(builder.ToString());
		}
	}
}

