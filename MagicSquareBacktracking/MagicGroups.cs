using System;
using System.Collections.Generic;
using System.Linq;

namespace MagicSquareBacktracking
{
	public class MagicGroups
	{
		private int Size {	get; set;}

		private int _square;

		private int Square
		{
			get
			{ 
				if (_square == 0) 
				{
					_square = Size * Size;
				}

				return _square; 
			}
		}

		int _magicNumber;

		private int MagicNumber{
			get
			{ 
				if (_magicNumber == 0) 
				{
					_magicNumber = Size * (Size * Size + 1) / 2;
				}
				return _magicNumber;
			}
		}

		private List<List<int>> _listOfMagicGroups;

		private List<List<int>> ConstructList ()
		{
			List<List<int>> list = DerivedGroups(new List<int>());

			return list;
		}

		private List<List<int>> DerivedGroups(List<int> group)
		{
			List<List<int>> list = new List<List<int>> ();
			int l = group.Count;
			int sum = group.Sum();

			int min;
			if (l == 0) {
				min = 1;
			} 
			else 
			{
				min = group.Last() + 1;
			}

			int rem = Square - Size + l + 1;
			if (min > rem) {
				return new List<List<int>>();
			}

			for(int i = min; i < Square + 1; i++)
			{
				if(sum + i > MagicNumber)
				{
					continue;
				}

				if (l + 1 == Size) {
					if (sum + i == MagicNumber) 
					{
						List<int> aux = group.Clone ();
						aux.Add (i);
						list.Add (aux);
					}
				} 
				else 
				{
					List<int> aux = group.Clone ();
					aux.Add (i);
					list.Add (aux);
				}
			}

			if(l + 1 == Size)
			{
				return list;
			}

			List<List<int>> hyperlist = new List<List<int>> ();
			foreach (var m in list) {
				hyperlist.AddRange (DerivedGroups(m));
			}

			return hyperlist;
		}

		public List<List<int>> ListOfMagicGroups
		{
			get
			{ 
				if (_listOfMagicGroups == null) {
					_listOfMagicGroups = ConstructList ();
				}

				return _listOfMagicGroups;
			}
		}

		public MagicGroups (int size)
		{
			Size = size;
		}


	}
}

