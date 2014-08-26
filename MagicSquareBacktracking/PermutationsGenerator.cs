using System;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquareBacktracking
{
	public class PermutationsGenerator
	{
		public static List<T[]> Permutations<T>(IEnumerable<T> numbers)
		{
			if (numbers.Count() == 1)
			{
				return new List<T[]>{ numbers.ToArray() };
			}

			List<T[]> result = new List<T[]> ();

			for (int i = 0; i < numbers.Count (); i++)
			{
				List<T> rest = new List<T>();
				for (int j = 0; j < numbers.Count (); j++)
				{
					if (i == j)
					{
						continue;
					}

					rest.Add (numbers.ElementAt(j));
				}

				List<T[]> subPerms = Permutations (rest);
				foreach (var p in subPerms) {
					var res = new List<T>{numbers.ElementAt(i)};
					res.AddRange(p);
					result.Add (res.ToArray ());
				}
			}

			return result;
		}
	}
	
}