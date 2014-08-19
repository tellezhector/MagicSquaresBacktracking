using System;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquareBacktracking
{
	public class MagicSquares
	{
		private List<List<int>> ListOfMagicGroups { get; set; }

		int Size { get; set; }

		public MagicSquares(int size)
		{
			Size = size;
			ListOfMagicGroups = new MagicGroups (size).ListOfMagicGroups;
		}

		public void PrintMagicSquares(){

		}

		void TryPermutations (List<List<int>> current)
		{
			throw new NotImplementedException ();
		}

		public void Backtrack(List<List<int>> current, int index){
			if (current.Count == Size) 
			{
				TryPermutations (current);
			}

			for (int i = index; i < current.Count; i++)
			{
				if(Accepts(current, current[i]))
				{
					Backtrack (current.Concat (new List<List<int>>{ current [i] }).ToList(), i+1);
				}
			}
		}

		bool Accepts (List<List<int>> current, List<int> list)
		{
			throw new NotImplementedException ();
		}
	}
	
}