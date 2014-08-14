using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquareBacktracking
{
	public static class Extensions
	{
		public static List<T> Clone<T>(this IList<T> listToClone) where T : struct
		{
			return listToClone.Select(item => item).ToList();
		}
	}
}

