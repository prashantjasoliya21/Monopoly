using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopolly.Model
{
	internal class Position
	{
		public Position(string v, int initPositionX, int initPositionY)
		{
			Name = v;
			X = initPositionX;
			Y = initPositionY;
		}

		public string Name { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		
	}
}
