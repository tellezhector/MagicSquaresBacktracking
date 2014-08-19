using System;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquareBacktracking
{
	public class PermutationsGenerator
	{
		public static List<int[]> Permutations(int[] numbers)
		{
			if (numbers.Length == 1)
			{
				return new List<int[]>{ numbers };
			}

			List<int[]> result = new List<int[]> ();

			foreach (int i in numbers) {
				var rest = ((int[])numbers.Clone ()).Where(n => n != i).ToArray();
				var perms = Permutations(rest);
				foreach (var p in perms) {
					var res = new List<int>{i};
					res.AddRange(p);
					result.Add (res.ToArray ());
				}
			}

			return result;
		}
	}
	
}