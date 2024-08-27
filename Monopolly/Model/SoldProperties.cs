using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopolly.Model
{
	 class SoldProperties
	{
		public string PlayerName { get; set; }
		public string PropertyName { get; set; }
		public int Level { get; set; }
		public SoldProperties(string name, string pname, int level)
		{
			PlayerName = name;
			PropertyName = pname;
			Level = level;
		}
	}
}
