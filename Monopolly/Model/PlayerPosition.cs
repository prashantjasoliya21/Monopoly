using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopolly.Model
{
	internal class PlayerPosition
	{
		public PlayerPosition( int index, int currentPosition,int balance)
		{
			Index = index;
			CurrentPosition = currentPosition;
			Balance = balance;
		}

		public int Index { get; set; }
		public int CurrentPosition { get; set; }
		public int Balance { get; set; }
	}
}
